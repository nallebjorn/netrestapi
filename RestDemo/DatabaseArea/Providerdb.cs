using System;
using RestDemo.Models;

namespace RestDemo.DatabaseArea
{
    public class Providerdb
    {
        public Boolean addProvider(Provider provider)
        {
            var query =
                "INSERT INTO `providers` (`username`, `provider_name`, `provider_surename`, `provider_address`, `create_date`, `update_date`) VALUES ('" +
                provider.user.username + "', '" + provider.name + "', '" + provider.surename + "', '" +
                provider.address + "', current_timestamp(), NULL)";
            Console.WriteLine(query);
            var cmd = DbCommand.create(query);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}