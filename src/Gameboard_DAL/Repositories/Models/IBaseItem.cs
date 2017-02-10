using System;

namespace Gameboard_DAL.Repositories.Models
{
    public interface IBaseItem
    {
        //Id, No, Unique
        string Id { get; set; }
        //Name, No, Max length 50
        string Name { get; set; }
        // creation datetime
        DateTime? CreationTime { get; set; }
        // last modification datetime
        DateTime? LastModified { get; set; }

        void FromInterface(IBaseItem item);
    }
}