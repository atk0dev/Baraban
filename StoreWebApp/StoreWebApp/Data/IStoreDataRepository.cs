using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApp.Data
{
  public interface IStoreDataRepository
  {
    IQueryable<Topic> GetTopics();
    IQueryable<Topic> GetTopicsIncludingReplies();
    IQueryable<Reply> GetRepliesByTopic(int topicId);
    
      IQueryable<Group> GetGroups();
      IQueryable<Group> GetGroupsIncludingItems();
      IQueryable<Item> GetItems(int groupId);
      IQueryable<Detail> GetDetails(int itemId); 

    bool Save();

    bool AddTopic(Topic newTopic);
    bool AddReply(Reply newReply);
    bool AddGroup(Group newGroup);
    Group EditGroup(Group updGroup);

      bool DeleteGroup(int id);
      bool AddItem(Item newItem);
      bool DeleteItem(int id);
      Group GetGroupById(int id);

      Item EditItem(Item updItem);

      bool AddDetail(Detail newDetail);
      bool DeleteDetail(int id);
  }
}
