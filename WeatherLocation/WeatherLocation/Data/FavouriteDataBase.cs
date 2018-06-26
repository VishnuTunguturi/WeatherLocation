using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherLocation.Models;

namespace WeatherLocation.Data
{
    public class FavouriteDataBase
    {
        readonly SQLiteAsyncConnection database;

        public FavouriteDataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<FavouriteDataTable>().Wait();
        }

        public Task<List<FavouriteDataTable>> GetItemsAsync()
        {
            return database.Table<FavouriteDataTable>().ToListAsync();
        }

        public Task<FavouriteDataTable> GetItemAsync(int id)
        {
            return database.Table<FavouriteDataTable>().Where(i => i.PK == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(FavouriteDataTable item)
        {
            if (item.PK != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(FavouriteDataTable item)
        {
            return database.DeleteAsync(item);
        }
    }
}
