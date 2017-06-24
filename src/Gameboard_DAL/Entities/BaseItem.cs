using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gameboard_DAL.Entities
{
    public class BaseItem: IBaseItem
    {
        private string _id;
        private DateTime? _creationTime;
        private DateTime? _lastModified;

        public string Id
        {
            get { return _id ?? Guid.NewGuid().ToString(); }
            set { _id = value; }
        }

        public string Name { get; set; }

        public DateTime? CreationTime
        {
            get { return _creationTime ?? DateTime.UtcNow; }
            set { _creationTime = value; }
        }

        public DateTime? LastModified
        {
            get { return _lastModified ?? DateTime.UtcNow; }
            set { _lastModified = value; }
        }
    }
}
