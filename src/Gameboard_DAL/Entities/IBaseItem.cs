using System;

namespace Gameboard_DAL.Entities
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
    }
}