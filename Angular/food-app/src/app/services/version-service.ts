export interface VersionBase<X> {
    versionNo: number;
    areVersionsIentical(lhs: X, rhs: X) :boolean;
    clone(item: X) : X;
} 

export class VersionService<T> {

    versionHistory: Array<T extends VersionBase<T> > = new Array<T extends VersionBase<T>();
    currentVersionNo :number;
    maxNoOfVersions: number = 20;
    currentVersion: T extends VersionBase<T>;

    VersionService(versionedItem: T extends VersionBase<T>) {}

    newVersion(item: T extends VersionBase<T> ) {
        if ((!(item.versionNo == undefined) && (this.currentVersion.versionNo == undefined))
        && !this.areVersionsIentical(item, this.currentVersion)
        && ((item.versionNo ?? 0 ) == 0))
        {
            var itemCopy = this.clone(item);
            itemCopy.versionNo = this.currentVersionNo;
            var maxVersionNo = Math.max( ...this.versionHistory.map(v=>v.versionNo) );
            if (!isNaN(maxVersionNo))
            {
                var newVersionList = this.versionHistory.filter(v=>v.versionNo <= this.currentVersion.versionNo);
                if (this.versionHistory.length > 0)
                {
                    this.versionHistory = newVersionList;
                }
            }
            while (this.versionHistory.length >= this.maxNoOfVersions)
            {
                this.versionHistory.shift;
            }            
            this.versionHistory.push(item);
            this.currentVersion = itemCopy;
        }
        else
        {
            this.currentVersion = this.clone(item);
        }
    }

}