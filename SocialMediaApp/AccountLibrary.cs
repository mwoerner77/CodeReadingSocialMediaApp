namespace SocialMediaApp
{
    using Microsoft.Extensions.Caching.Memory;
    using SocialMediaApp.Models;
    using System.Text.Json;

    public class AccountLibrary
    {
        private readonly IMemoryCache recentAccountsCache;
        private List<Account?>? accounts = new List<Account?>();
        private static AccountLibrary accountLibraryInstance = new AccountLibrary();

        //29:
        //
        private AccountLibrary()
        {
            MemoryCacheOptions cacheOptions = new MemoryCacheOptions();
            this.recentAccountsCache = new MemoryCache(cacheOptions);      
        }

        //30:
        //
        public static AccountLibrary GetAccountLibrarySingleInstance()
        {
            return accountLibraryInstance;
        }

        public bool TryGetAccount(string username, out Account? account)
        {
            //31:
            //
            if (this.recentAccountsCache.TryGetValue(username, out Account? acc))
            {
                account = acc;
                return true;
            }

            //32:
            //
            this.FetchAllAccounts();

            //33:
            //
            if (this.accounts != null)
            {
                //34:
                //
                foreach (Account? acc1 in this.accounts)
                {
                    if (acc1 != null && acc1.Username != null && acc1.Username == username)
                    {
                        account = acc1;
                        return true;
                    }
                }
            }

            account = null;
            return false;
        }

        public void AddNewAccount(string username, int password)
        {
            //35:
            //
            Account newAccount = new Account()
            {
                Username = username,
                Password = password.ToString()
            };

            //36:
            //
            this.FetchAllAccounts();

            //37:
            //
            this.accounts?.Add(newAccount);
            string accountsJsonText = JsonSerializer.Serialize(this.accounts);
            File.WriteAllText("Accounts.json", accountsJsonText);

            //38:
            //
            this.recentAccountsCache.Set(username, newAccount);
        }

        public void AddNewAccount(string username, string password)
        {
            //39:
            //
            Account newAccount = new Account()
            {
                Username = username,
                Password = password
            };

            //40:
            //
            this.FetchAllAccounts();

            //41:
            //
            this.accounts?.Add(newAccount);
            string accountsJsonText = JsonSerializer.Serialize(this.accounts);
            File.WriteAllText("Accounts.json", accountsJsonText);

            //42:
            //
            this.recentAccountsCache.Set(username, newAccount);
        }

        //43:
        //
        private void FetchAllAccounts()
        {
            string accountsJsonText = File.ReadAllText("Accounts.json");
            this.accounts = JsonSerializer.Deserialize<List<Account?>>(accountsJsonText);
        }
    }
}
