using lkcode.hetznercloudapi.Core;
using lkcode.hetznercloudapi.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lkcode.hetznercloudapi.Api
{
    public class PrivateNetwork
    {
        #region # static properties #

        private static int _currentPage { get; set; }
        public static int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
            }
        }

        private static int _maxPages { get; set; }
        public static int MaxPages
        {
            get
            {
                return _maxPages;
            }
            set
            {
                _maxPages = value;
            }
        }

        #endregion

        #region # public properties #

        private long _id { get; set; }
        /// <summary>
        /// ID of the network.
        /// </summary>
        public long Id
        {
            get
            {
                return this._id;
            }
            private set
            {
                this._id = value;
            }
        }

        private string _name { get; set; }
        /// <summary>
        /// Unique identifier of the network.
        /// </summary>
        public string Name
        {
            get
            {
                return this._name;
            }
            private set
            {
                this._name = value;
            }
        }

        #endregion

        #region # static methods #

        /// <summary>
        /// Returns all datacenter in a list.
        /// </summary>
        /// <returns></returns>
        public static async Task<List<PrivateNetwork>> GetAsync(int page = 1, string token = null)
        {
            if ((_maxPages > 0 && (page <= 0 || page > _maxPages)))
            {
                throw new InvalidPageException("invalid page number (" + page + "). valid values between 1 and " + _maxPages + "");
            }

            List<PrivateNetwork> networksList = new List<PrivateNetwork>();

            string url = string.Format("/networks");
            if (page > 1)
            {
                url += "?page=" + page.ToString();
            }

            string responseContent = await ApiCore.SendRequest(url, token);
            var response = JsonConvert.DeserializeObject<Objects.Network.Get.Response>(responseContent);

            // load meta
            CurrentPage = response.meta.pagination.page;
            float pagesDValue = ((float)response.meta.pagination.total_entries / (float)response.meta.pagination.per_page);
            MaxPages = (int)Math.Ceiling(pagesDValue);

            foreach (var network in response.networks)
            {
                networksList.Add(GetNetworkFromResponseData(network));
            }

            return networksList;
        }

        /// <summary>
        /// Returns all datacenter with the given id.
        /// </summary>
        /// <returns></returns>
        public static async Task<PrivateNetwork> GetAsync(long id, string token = null)
        {
            var network = new PrivateNetwork();

            string url = string.Format("/networks/{0}", id);

            string responseContent = await ApiCore.SendRequest(url, token);
            var response = JsonConvert.DeserializeObject<Objects.Network.GetOne.Response>(responseContent);

            network = GetNetworkFromResponseData(response.network);

            return network;
        }

        #endregion

        #region # public methods #

        public PrivateNetwork()
        {

        }

        public PrivateNetwork(int id)
        {
            this._id = id;
        }

        #endregion

        #region # private methods for processing #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseData"></param>
        /// <returns></returns>
        private static PrivateNetwork GetNetworkFromResponseData(Objects.Network.Universal.Network responseData)
        {
            var network = new PrivateNetwork();

            network._id = responseData.id;
            network.Name = responseData.name;

            return network;
        }

        #endregion
    }
}
