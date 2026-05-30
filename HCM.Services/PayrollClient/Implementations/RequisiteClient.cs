using HCM.Models.Models;
using HCM.Services.PayrollClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Services.PayrollClient.Implementations
{
    public class RequisiteClient : IRequisiteClient
    {
        private readonly HttpClient _http;
        public RequisiteClient(HttpClient http) => _http = http;

        public Task AddAsync(RequisiteDetails req)
        {
            var resp = _http.PostAsJsonAsync("api/Masters", req);
            return resp.ContinueWith(t =>
            {
                t.Result.EnsureSuccessStatusCode();
                return t.Result.Content.ReadFromJsonAsync<Requesites>().ContinueWith(ct =>
                {
                    var created = ct.Result;
                    if (created != null) req.RequisiteDetailsId = created.ReqId;
                });
            }).Unwrap();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RequisiteDetails>> GetAllAsync()
        {
           var result = _http.GetFromJsonAsync<IEnumerable<RequisiteDetails>>("api/Masters");
            return result.ContinueWith(t => t.Result ?? Enumerable.Empty<RequisiteDetails>());
        }

        public Task<Requesites?> GetByIdAsync(int id)
        {
            var result = _http.GetAsync($"api/Masters/{id}").ContinueWith(t =>
            {
                var resp = t.Result;
                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<Requesites>();
                else if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return Task.FromResult<Requesites?>(null);
                else
                    throw new HttpRequestException($"Unexpected status code: {resp.StatusCode}");
            }).Unwrap();
            return result;
        }

        public Task<IEnumerable<Requesites>> GetRequesites()
        {
            var result = _http.GetFromJsonAsync<IEnumerable<Requesites>>("api/Masters/requesites");
            return result.ContinueWith(t => t.Result ?? Enumerable.Empty<Requesites>());
        }

        public Task UpdateAsync(RequisiteDetails req)
        {
           var resp = _http.PutAsJsonAsync($"api/Masters/{req.RequisiteDetailsId}", req);
            return resp.ContinueWith(t =>
            {
                t.Result.EnsureSuccessStatusCode();
            });
        }
    }
}
