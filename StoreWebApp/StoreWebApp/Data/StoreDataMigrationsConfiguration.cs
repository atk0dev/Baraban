using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace StoreWebApp.Data
{
  public class StoreDataMigrationsConfiguration
    : DbMigrationsConfiguration<StoreDataContext>
  {
    public StoreDataMigrationsConfiguration()
    {
      this.AutomaticMigrationDataLossAllowed = true;
      this.AutomaticMigrationsEnabled = true;
    }

    protected override void Seed(StoreDataContext context)
    {
      base.Seed(context);
        
#if DEBUG
      if (context.Topics.Count() == 0)
      {
        var topic = new Topic()
        {
          Title = "I love MVC",
          Created = DateTime.Now,
          Body = "I love ASP.NET MVC and I want everyone to know it",
          Replies = new List<Reply>()
          {
            new Reply()
            {
               Body = "I love it too!",
               Created = DateTime.Now
            },
            new Reply()
            {
               Body = "Me too",
               Created = DateTime.Now
            },
            new Reply()
            {
               Body = "Aw shucks",
               Created = DateTime.Now
            },
          }
        };

        context.Topics.Add(topic);

        var anotherTopic = new Topic()
        {
          Title = "I like Ruby too!",
          Created = DateTime.Now,
          Body = "Ruby on Rails is popular"
        };

        context.Topics.Add(anotherTopic);

        if (context.Groups.Count() == 0)
        {
            context.Groups.Add(new Group { Title = "Group 1", ShowOrder = 1, Link = string.Format("#/group/{0}", 1), Body = "Lorem ipsum dolor sit amet", Visible = true});
            context.Groups.Add(new Group { Title = "Group 2", ShowOrder = 10, Link = "#/group/2", Body = "Lorem ipsum dolor sit amet", Visible = true });
            context.Groups.Add(new Group { Title = "Group 3", ShowOrder = 11, Link = "#/group/3", Body = "Lorem ipsum dolor sit amet", Visible = true });

                   }

        if (context.Items.Count() == 0)
        {
            context.Items.Add(new Item { Title = "Some 1", Image = "/Static/CatalogImages/img.jpg", Descr = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", GroupId = 1, Price = 10 });
            context.Items.Add(new Item { Title = "Some 2", Image = "/Static/CatalogImages/img.jpg", Descr = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", GroupId = 1, Price = 11.2M });
            context.Items.Add(new Item { Title = "Some 3", Image = "/Static/CatalogImages/img.jpg", Descr = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", GroupId = 2, Price = 12 });
            context.SaveChanges();
        }

        if (context.Details.Count() == 0)
        {
            context.Details.Add(new Detail { Image = "/Static/CatalogImages/img.jpg", Title = "Sub1", ItemId = 1 });
            //context.Details.Add(new Detail { Image = "/Static/CatalogImages/img.jpg", Title = "Sub2", ItemId = 1 });
            //context.Details.Add(new Detail { Image = "/Static/CatalogImages/img.jpg", Title = "Sub3", ItemId = 1 });
            //context.Details.Add(new Detail { Image = "/Static/CatalogImages/img.jpg", Title = "Sub4", ItemId = 1 });

            //context.Details.Add(new Detail { Image = "/Static/CatalogImages/img.jpg", Title = "Sub1", ItemId = 2 });
            //context.Details.Add(new Detail { Image = "/Static/CatalogImages/img.jpg", Title = "Sub2", ItemId = 2 });
            //context.Details.Add(new Detail { Image = "/Static/CatalogImages/img.jpg", Title = "Sub3", ItemId = 2 });
        }


        if (context.Parameters.Count() == 0)
        {
            context.Parameters.Add(new Parameter() { Name = "0group", Value = "1", Descr = "Group number displayed as default page for catalog"});
        }

        try
        {
          context.SaveChanges();
        }
        catch (Exception ex)
        {
          var msg = ex.Message;
        }
      }
#endif
        
    }
  }
}
