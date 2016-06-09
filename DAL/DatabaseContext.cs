
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnualReportCrawler.DAL
{
    public class DatabaseContext
    {
        IMongoDatabase database;
        public DatabaseContext()
        {
            var client = new MongoClient("");

            database = client.GetDatabase("Company");


        }
    }
}
