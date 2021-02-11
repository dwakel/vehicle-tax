//using Microsoft.AspNetCore.Http;
//using System;
//using System.Text.Json;

//namespace VehicleTax.Services
//{
//    public interface IAuthenticationSessionProvider
//    {
//        void SetAuthenticatedSession(long userId, string authKey);

//        void DeleteSessionData();
//    }

//    public class AuthenticationSessionProvider : IAuthenticationSessionProvider
//    {
//        private const string AuthCookieName = "_tracker.auth";
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private ISession _session => _httpContextAccessor.HttpContext.Session;

//        public AuthenticationSessionProvider(IHttpContextAccessor httpContextAccessor)
//        {
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public void SetAuthenticatedSession(long userId, string authKey)
//        {

//            _session.SetString(AuthCookieName, JsonSerializer.Serialize(
//            new SessionData { 
//                UserId = userId,
//                AuthKey = authKey
//            }));
//        }

//        public SessionData GetSessionData()
//        {
//            string value = _session.GetString(AuthCookieName);
//            return value == null ? default : JsonSerializer.Deserialize<SessionData>(value);
//        }

//        public void DeleteSessionData()
//        {
//            throw new NotImplementedException();
//        }

//    }

//    public class SessionData
//    {
//        public long UserId { get; set; }
//        public string AuthKey { get; set; }
//    }
//}
