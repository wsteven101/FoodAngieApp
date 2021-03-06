======================================
CREATE THE KUBERNETES CLUSTER ON AZURE
======================================
#From an azure command line do....

# create the cluster
    az aks create --resource-group FoodApp --name FoodAppKube1 --node-count 1 --enable-addons monitoring --generate-ssh-keys --attach-acr FoodAppRegistry -l uksouth

# then connect to the K8 service
     az aks get-credentials --resource-group FoodApp --name FoodAppKube1

# then update the k8 service to be able to access the docker repository
     az aks update -n FoodAppKube1 -g FoodApp --attach-acr FoodAppRegistry

===========================
#Deploy the FoodAngie app to the cluster FoodAppKube1
#Either do this via Azure DevOps with pipline file or do it manually on the azure command line woth....



--note the ingress is setup is the yaml file


===========================
CREATE THE GATEWAY/INGRESS  - azure add on
===========================

az aks create -n FoodAppKube1 -g FoodApp --network-plugin azure --enable-managed-identity -a ingress-appgw --appgw-name agic --appgw-subnet-cidr "10.2.0.0/16" --generate-ssh-keys  --attach-acr FoodAppRegistry -l uksouth
az aks get-credentials --resource-group FoodApp --name FoodAppKube1
az aks get-credentials --resource-group FoodApp --name FoodAppKube1
kubectl create -f ./food-system.yaml
kubectl create -f ./food-system.yaml
kubectl get ingress

===========================
CREATE THE GATEWAY/INGRESS  AND KEY VAULT - azure add on
===========================

>>add azure ad pod managed id

az feature register --name EnablePodIdentityPreview --namespace Microsoft.ContainerService
# Install the aks-preview extension
az extension add --name aks-preview
# Update the extension to make sure you have the latest version installed
az extension update --name aks-preview
# check preview is installled
az feature show --name EnablePodIdentityPreview --namespace Microsoft.ContainerService -o table
# refrefresh the registration of the namespace before creating the custer
az provider register --namespace Microsoft.ContainerService

>> key vault
az aks create -n FoodAppKube1 -g FoodApp --network-plugin azure --enable-addons azure-keyvault-secrets-provider  --enable-managed-identity --enable-pod-identity  -a ingress-appgw --appgw-name agic --appgw-subnet-cidr "10.2.0.0/16" --generate-ssh-keys  --attach-acr FoodAppRegistry -l uksouth
az aks create -n FoodAppKube1 -g FoodApp --network-plugin azure --enable-addons azure-keyvault-secrets-provider  --enable-managed-identity --enable-pod-identity--generate-ssh-keys  --attach-acr FoodAppRegistry -l uksouth
#az aks get-credentials --resource-group FoodApp --name FoodAppKube1
kubectl get pods -n kube-system -l 'app in (secrets-store-csi-driver, secrets-store-provider-azure)'

user-assigned-id:aad-pod-id
clientid:ea843455-3b2f-4861-8193-50713ee7387a
objectid:be8bc5c7-5466-427d-b555-055b0ab5983c
resourceid:/subscriptions/dabf6320-db56-447e-86c4-bb677d87a890/resourceGroups/FoodApp/providers/Microsoft.ManagedIdentity/userAssignedIdentities/aad-pod-id

az group create --name myIdentityResourceGroup --location uksouth
export IDENTITY_RESOURCE_GROUP="myIdentityResourceGroup"
export IDENTITY_NAME="application-identity"
az identity create --resource-group ${IDENTITY_RESOURCE_GROUP} --name ${IDENTITY_NAME}
export IDENTITY_CLIENT_ID="$(az identity show -g ${IDENTITY_RESOURCE_GROUP} -n ${IDENTITY_NAME} --query clientId -otsv)"
export IDENTITY_RESOURCE_ID="$(az identity show -g ${IDENTITY_RESOURCE_GROUP} -n ${IDENTITY_NAME} --query id -otsv)"

NODE_GROUP=$(az aks show -g FoodApp -n FoodAppKube1 --query nodeResourceGroup -o tsv)
NODES_RESOURCE_ID=$(az group show -n $NODE_GROUP -o tsv --query "id")
az role assignment create --role "Virtual Machine Contributor" --assignee "$IDENTITY_CLIENT_ID" --scope $NODES_RESOURCE_ID

export POD_IDENTITY_NAME="my-pod-identity"
export POD_IDENTITY_NAMESPACE="my-app"
#az aks pod-identity add --resource-group FoodApp --cluster-name FoodAppKube1 --namespace ${POD_IDENTITY_NAMESPACE}  --name ${POD_IDENTITY_NAME} --identity-resource-id ${IDENTITY_RESOURCE_ID}
az aks pod-identity add --resource-group FoodApp --cluster-name FoodAppKube1 --namespace default --name aad-pod-id --identity-resource-id /subscriptions/dabf6320-db56-447e-86c4-bb677d87a890/resourceGroups/FoodApp/providers/Microsoft.ManagedIdentity/userAssignedIdentities/aad-pod-id


===========================
CREATE THE GATEWAY/INGRESS - the hard way
===========================

# Create the gateway under a new group name e.g. 'agic'

az group create -n agic -l uksouth
az network public-ip create -n agic-pip -g agic --allocation-method Static --sku Standard --dns-name foodangie
az network vnet create -n agic-vnet -g agic --address-prefix 192.168.0.0/24 --subnet-name agic-subnet --subnet-prefix 192.168.0.0/24
az network application-gateway create -n  agic -l uksouth -g  agic --sku Standard_v2 --public-ip-address agic-pip --vnet-name agic-vnet --subnet agic-subnet

Enable integration between the gateway 'agic' and the 'FoodAppKube1' cluster

appgwId=$(az network application-gateway show -n agic -g agic -o tsv  --query  "id")
echo $appgwId
az aks enable-addons -n FoodAppKube1 -g FoodApp -a ingress-appgw --appgw-id $appgwId
  
# Peer the gateway with the AKS network 

nodeResourceGroup=$(az aks show -nFoodAppKube1 -g FoodApp -o tsv --query "nodeResourceGroup")
echo $nodeResourceGroup
aksVnetName=$(az network vnet list -g $nodeResourceGroup -o tsv --query "[0].name")
aksVnetId=$(az network vnet show -n $aksVnetName -g $nodeResourceGroup -o tsv --query "id")
echo $aksVnetName
echo $aksVnetId
az network vnet peering create -n AppGWtoAKSVnetPeering -g agic --vnet-name agic-vnet --remote-vnet $aksVnetId --allow-vnet-access
appGWVnetId=$(az network vnet show  -n agic-vnet -g agic -o tsv --query "id")
echo $appGWVnetId
az network vnet peering create  -n AKStoAppGWVnetPeering -g $nodeResourceGroup --vnet-name $aksVnetName --remote-vnet $appGWVnetId --allow-vnet-access

#deploy the gateway

kubectl apply -f ./food-system-ingress.yaml

=======================
DELETE THE CLUSTER
=======================

az aks delete --resource-group FoodApp --name FoodAppKube1

az aks delete --resource-group FoodApp --name food-system-ingress

az network application-gateway delete -n  agic -g  agic

az group delete --name agic

az aks pod-identity delete --name ${POD_IDENTITY_NAME} --namespace ${POD_IDENTITY_NAMESPACE} --resource-group myResourceGroup --cluster-name myAKSCluster

az identity delete -g ${IDENTITY_RESOURCE_GROUP} -n ${IDENTITY_NAME}


=======================
helm repo add csi-secrets-store-provider-azure https://raw.githubusercontent.com/Azure/secrets-store-csi-driver-provider-azure/master/charts

helm install csi-secrets-store-provider-azure/csi-secrets-store-provider-azure --generate-name --set secrets-store-csi-driver.syncSecret.enabled=tru

clientId=`az aks show --name FoodAppKube1 --resource-group FoodApp |jq -r .identityProfile.kubeletidentity.clientId`
nodeResourceGroup=`az aks show --name FoodAppKube1 --resource-group FoodApp |jq -r .nodeResourceGroup` 
subId=`az account show | jq -r .id`

az role assignment create --role "Managed Identity Operator" --assignee $clientId --scope /subscriptions/$subId/resourcegroups/FoodApp 
az role assignment create --role "Managed Identity Operator" --assignee $clientId --scope /subscriptions/$subId/resourcegroups/$nodeResourceGroup
az role assignment create --role "Virtual Machine Contributor" --assignee $clientId --scope /subscriptions/$subId/resourcegroups/$nodeResourceGroup

helm repo add aad-pod-identity https://raw.githubusercontent.com/Azure/aad-pod-identity/master/charts
helm install pod-identity aad-pod-identity/aad-pod-identity

az identity create -g FoodApp -n aks2kvFoodAppIdentity

#may need to download azactivedir_deploy.yml from FoodAngie dir first
kubectl apply -f ./azactivedir_deploy.yml
	
######################################

az feature register --name EnablePodIdentityPreview --namespace Microsoft.ContainerService

# Install the aks-preview extension
az extension add --name aks-preview

# Update the extension to make sure you have the latest version installed
az extension update --name aks-preview

az aks create --resource-group FoodApp --name FoodAppKube1 --node-count 1 --enable-addons monitoring --generate-ssh-keys --attach-acr FoodAppRegistry --enable-pod-identity --network-plugin azure

#       then connect to the K8 service
az aks get-credentials --resource-group FoodApp --name FoodAppKube1

#       then update the k8 service to be able to access the docker repository
az aks update -n FoodAppKube1 -g FoodApp --attach-acr FoodAppRegistry


az aks update  -n FoodAppKube1 -g FoodApp  --enable-pod-identity

# setup azure policy for the FoodApp group setting it to "Kubernetes cluster pod security baseline standards for Linux-based workloads"
# see https://docs.microsoft.com/en-us/azure/aks/use-azure-policy

# check that the policy is in operation (can take 20 mins or so to show


