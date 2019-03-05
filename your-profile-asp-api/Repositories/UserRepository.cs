using aspApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using aspApi.Services;
using AutoMapper.EntityFrameworkCore;

namespace aspApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _contexto;
        private readonly IConfiguration _configuration;
        private readonly ImageService imageService;

        public UserRepository(UserDbContext context, IConfiguration configuration)
        {
            _contexto = context;
            _configuration = configuration;
            imageService = new ImageService(configuration);
        }

        public User Add(User user)
        {
            user.ProfileImage = "https://lfmsyssotrage.blob.core.windows.net/cover-images/default.jpg";
            user.CoverImage = "https://lfmsyssotrage.blob.core.windows.net/profile-images/default.jpg";

            user.Password = GetMd5Hash(user.Password);

            _contexto.Users.Add(user);
            _contexto.SaveChanges();
            return user;
        }

        public string createToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["SecurityKey"],
              claims: new[] { new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()) },
              signingCredentials: creds, expires: DateTime.Now.AddYears(1));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public User Find(int? id)
        {
            return _contexto.Users.FirstOrDefault(u => u.Id == id);
        }


        public User FindByEmail(string email)
        {
            return _contexto.Users.Where(u => u.Email == email).FirstOrDefault();
        }
        public string Authenticate(Credentials credentials)
        {
            var user = _contexto.Users.FirstOrDefault(u => u.Email == credentials.email);

            return (user != null && ValidatePassword(credentials.password, user.Password)) ? createToken(user) : null;
        }

        bool ValidatePassword(string input, string hash)
        {
            string hashOfInput = GetMd5Hash(input);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return (0 == comparer.Compare(hashOfInput, hash)) ? true : false;
        }

        public async Task uploadProfileImage(int userId, string image)
        {
            try
            {
                AppImage appImage = new AppImage();

                appImage.fileName = userId.ToString() + ".jpeg";
                appImage.image = Convert.FromBase64String(image);
                appImage.storage = Storage.ProfileStorage;

                var profileUrl = await imageService.SaveFile(appImage);

                var user = await _contexto.Users.FindAsync(userId);

                user.ProfileImage = profileUrl;
                await _contexto.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task uploadCoverImage(int userId, string image)
        {
            try
            {
                AppImage appImage = new AppImage();

                var bytes = Convert.FromBase64String(image);

                appImage.fileName = userId.ToString() + ".jpeg";
                appImage.image = Convert.FromBase64String(image);
                appImage.storage = Storage.CoverStorage;

                var coverUrl = await imageService.SaveFile(appImage);
                var user = await _contexto.Users.FindAsync(userId);
                user.CoverImage = coverUrl;
                await _contexto.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

       public string GetMd5Hash(string input)
        {
            using (MD5 encoder = MD5.Create())
            {

                byte[] data = encoder.ComputeHash(Encoding.UTF8.GetBytes(input + _configuration["SecurityKey"]));
                StringBuilder value = new StringBuilder();

                for (long i = 0; i < data.Length; i++)
                {
                    value.Append(data[i].ToString());
                }

                return value.ToString();
            }

        }

        public void UpdatePassword(User user) {

            var _user = _contexto.Users.Find(user.Id);

            _user.Password = GetMd5Hash(user.Password);

            _contexto.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _contexto.Users.ToList();
        }

        public void Remove(int id)
        {
            var entity = _contexto.Users.First(u => u.Id == id);
            _contexto.Users.Remove(entity);
            _contexto.SaveChanges();
        }

        public void Update(User user)
        {
            try
            {

                var _user = _contexto.Users.Find(user.Id);

                _user.FirstName = user.FirstName;
                _user.LastName = user.LastName;
                _user.Gender = user.Gender;
                _user.BirthDate = user.BirthDate;
                _user.UpdatedAt = DateTimeOffset.Now;

                if (user.Address != null)
                {
                    Address _address = null;

                    _address = _contexto.Addresses.Find(user.AddressId);

                    if (_address == null)
                        _address = new Address();

                    _address.City = user.Address.City;
                    _address.Number = user.Address.Number;
                    _address.Neighborhood = user.Address.Neighborhood;
                    _address.Street = user.Address.Street;
                    _address.ZipCode = user.Address.ZipCode;
                    _address.UpdatedAt = DateTime.Now;

                    _contexto.Addresses.Update(_user.Address);


                    _contexto.Cities.Update(_user.Address.City);
                    _contexto.States.Update(_user.Address.City.State);
                    _contexto.Countries.Update(_user.Address.City.State.Country);

                    _user.Address = _address;
                }

                _contexto.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }
    }

}
