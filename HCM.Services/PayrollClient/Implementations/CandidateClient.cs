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
    public class CandidateClient : ICandidateClient
    {
        private readonly HttpClient _http;
        public CandidateClient(HttpClient http) => _http = http;

        public Task AddAsync(Candidate employee)
        {
            var resp = _http.PostAsJsonAsync("api/Candidate", employee);
            return resp.ContinueWith(t =>
            {
                t.Result.EnsureSuccessStatusCode();
                return t.Result.Content.ReadFromJsonAsync<Candidate>().ContinueWith(ct =>
                {
                    var created = ct.Result;
                    if (created != null) employee.CandidateId = created.CandidateId;
                });
            }).Unwrap();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Candidate>> GetAllAsync()
        {
            var result = _http.GetFromJsonAsync<IEnumerable<Candidate>>("api/Candidate");
            return result.ContinueWith(t => t.Result ?? Enumerable.Empty<Candidate>());
        }

        public Task<Candidate?> GetByIdAsync(int id)
        {
            var result = _http.GetAsync($"api/Candidate/{id}").ContinueWith(t =>
              {
                  var resp = t.Result;
                  if (resp.IsSuccessStatusCode)
                      return resp.Content.ReadFromJsonAsync<Candidate>();
                  else if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
                      return Task.FromResult<Candidate?>(null);
                  else
                      throw new HttpRequestException($"Unexpected status code: {resp.StatusCode}");
              }).Unwrap();
            return result;
        }

        public Task UpdateAsync(Candidate employee)
        {
            var resp = _http.PutAsJsonAsync($"api/Candidate/{employee.CandidateId}", employee);
            return resp.ContinueWith(t =>
            {
                t.Result.EnsureSuccessStatusCode();
            });
        }
    }
}
