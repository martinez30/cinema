using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.IO;

namespace Infra.Repository
{
    public abstract class DesignTimeDBContextFactoryBase<T> : IDesignTimeDbContextFactory<T> where T : DbContext
    {
        public T CreateDbContext(string[] args)
        {
            var basepath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}Teste.Web",Path.DirectorySeparatorChar);
            Configuration.Build(basepath);

            return Create(Configuration.ConnectionString);
        }

        private T Create(string connectionstring)
        {
            if (string.IsNullOrEmpty(connectionstring))
            {
                throw new ArgumentException("Connection String está vazia");
            }
            Console.WriteLine("Connection String: {0}",connectionstring);

            var optionbuilder = new DbContextOptionsBuilder<T>();

            optionbuilder.UseSqlServer(connectionstring);

            return CreateNewInstance(optionbuilder.Options);
        }

        protected abstract T CreateNewInstance(DbContextOptions<T> options);
    }
}
