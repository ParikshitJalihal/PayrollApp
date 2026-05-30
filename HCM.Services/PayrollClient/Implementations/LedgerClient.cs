using HCM.Models.Models;
using HCM.Services.PayrollClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HCM.Services.PayrollClient.Implementations
{
    public class LedgerClient : ILedgerClient
    {
        private readonly HttpClient _http;
        public LedgerClient(HttpClient http) => _http = http;

        public async Task<IEnumerable<LedgerEntry>> GetAllAsync()
        {
            var res = await _http.GetFromJsonAsync<IEnumerable<LedgerEntry>>("api/Ledger");
            return res ?? Enumerable.Empty<LedgerEntry>();
        }

        public async Task AddAsync(LedgerEntry entry)
        {
            var resp = await _http.PostAsJsonAsync("api/Ledger", entry);
            resp.EnsureSuccessStatusCode();
        }
    }
}