using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace StoreWebApp.Data
{
  public class StoreDataRepository : IStoreDataRepository
  {
    StoreDataContext _ctx;
    public StoreDataRepository(StoreDataContext ctx)
    {
      _ctx = ctx;
    }

    public IQueryable<Topic> GetTopics()
    {
      return _ctx.Topics;
    }

    public IQueryable<Reply> GetRepliesByTopic(int topicId)
    {
      return _ctx.Replies.Where(r => r.TopicId == topicId);
    }

    public IQueryable<Group> GetGroups()
    {
        return this._ctx.Groups;
    }

      public IQueryable<Item> GetItems(int groupId)
      {

          if (groupId == 0)
          {
              var defGroup = _ctx.Parameters.FirstOrDefault(p => p.Name.Equals("0group"));
              if (defGroup != null && defGroup.Value != null)
              {
                  int gr = 0;
                  if (Int32.TryParse(defGroup.Value, out gr))
                  {
                      return _ctx.Items.Include("Details").Where(i => i.GroupId == gr);
                  }

                  return _ctx.Items.Include("Details");
              }

              return _ctx.Items.Include("Details");
          }

          return _ctx.Items.Include("Details").Where(i => i.GroupId == groupId);
      }

    public bool Save()
    {
      try
      {
        return _ctx.SaveChanges() > 0;
      }
      catch (Exception ex)
      {
        // TODO log this error
        return false;
      }
    }

    public bool AddTopic(Topic newTopic)
    {
      try
      {
        _ctx.Topics.Add(newTopic);
        return true;
      }
      catch (Exception ex)
      {
        // TODO log this error
        return false;
      }
    }


    public IQueryable<Topic> GetTopicsIncludingReplies()
    {
      return _ctx.Topics.Include("Replies");
    }

    public IQueryable<Group> GetGroupsIncludingItems()
    {
        return _ctx.Groups.Include("Items");
    }


    public bool AddReply(Reply newReply)
    {
      try
      {
        _ctx.Replies.Add(newReply);
        return true;
      }
      catch (Exception ex)
      {
        // TODO log this error
        return false;
      }
    }


    public IQueryable<Detail> GetDetails(int itemId)
    {
        return _ctx.Details.Where(d => d.ItemId == itemId);

    }

    public bool AddGroup(Group newGroup)
    {
        try
        {
            _ctx.Groups.Add(newGroup);
            return true;
        }
        catch (Exception ex)
        {
            // TODO log this error
            return false;
        }
    }

    public Group EditGroup(Group updGroup)
    {
        try
        {
            var group = _ctx.Groups.FirstOrDefault(g => g.Id.Equals(updGroup.Id));
            if (group != null)
            {
                group.Body = updGroup.Body;
                group.Link = updGroup.Link;
                group.ShowOrder = updGroup.ShowOrder;
                group.Title = updGroup.Title;
                group.Visible = updGroup.Visible;
                return updGroup;    
            }

            return null;
        }
        catch (Exception ex)
        {
            // TODO log this error
            return null;
        }
    }

    public Item EditItem(Item updItem)
    {
        try
        {
            var item = _ctx.Items.FirstOrDefault(i => i.Id.Equals(updItem.Id));
            if (item != null)
            {
                item.Title = updItem.Title;
                item.Title = updItem.Title;
                item.Descr = updItem.Descr;
                item.GroupId = updItem.GroupId;
                item.Price = updItem.Price;
                return updItem;
            }

            return null;
        }
        catch (Exception ex)
        {
            // TODO log this error
            return null;
        }
    }

    public bool DeleteGroup(int id)
    {
        try
        {
            var group = _ctx.Groups.FirstOrDefault(g => g.Id.Equals(id));
            if (group != null)
            {
                //var updItems = _ctx.Items.Where(i => i.GroupId.Equals(id));
                //foreach (var i in updItems)
                //{
                //    i.GroupId = 0;
                //}

                _ctx.Groups.Remove(group);
                
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            // TODO log this error
            return false;
        }
    }

    public bool AddItem(Item newItem)
    {
        try
        {
            _ctx.Items.Add(newItem);
            return true;
        }
        catch (Exception ex)
        {
            // TODO log this error
            return false;
        }
    }

    public bool DeleteItem(int id)
    {
        try
        {
            var item = _ctx.Items.FirstOrDefault(i => i.Id.Equals(id));
            if (item != null)
            {
                _ctx.Items.Remove(item);

                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            // TODO log this error
            return false;
        }
    }

    public bool DeleteDetail(int id)
    {
        try
        {
            var item = _ctx.Details.FirstOrDefault(i => i.Id.Equals(id));
            if (item != null)
            {
                _ctx.Details.Remove(item);

                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            // TODO log this error
            return false;
        }
    }

    public Group GetGroupById(int id)
    {
        if (id == 0)
        {
            var defGroup = _ctx.Parameters.FirstOrDefault(p => p.Name.Equals("0group"));
            if (defGroup != null && defGroup.Value != null)
            {
                int gr = 0;
                if (Int32.TryParse(defGroup.Value, out gr))
                {
                    return _ctx.Groups.FirstOrDefault(g => g.Id == gr);
                }
            }

            return new Group
                       {
                           Id = 0,
                           Title = "Default title",
                           Body = "Default body"
                       };
        }

        return _ctx.Groups.FirstOrDefault(g => g.Id == id);
    }

    public bool AddDetail(Detail newDetail)
    {
        try
        {
            _ctx.Details.Add(newDetail);
            return true;
        }
        catch (Exception ex)
        {
            // TODO log this error
            return false;
        }
    }
  }
}