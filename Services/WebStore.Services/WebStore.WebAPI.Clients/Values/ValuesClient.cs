﻿using WebStore.Interfaces.TestAPI;
namespace WebStore.WebAPI.Clients.Values
{
    public class ValuesClient : IValuesService
    {

        public ValuesClient()
        {

        }
        public void Add(string value)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(int id, string value)
        {
            throw new NotImplementedException();
        }

        public string GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetValues()
        {
            throw new NotImplementedException();
        }
    }
}
