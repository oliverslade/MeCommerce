using AutoMapper;
using DataModels;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class UserRepository
    {
        private readonly MeCommerceDbContext _context = new MeCommerceDbContext();

        private readonly IMapper _mapper;

        public UserRepository()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
            }).CreateMapper();
        }

        public void CreateUser(AspNetUsers user)
        {
            _context.AspNetUsers.Add(user);
            _context.SaveChanges();
        }

        public IEnumerable<AspNetUsers> GetAllUsers()
        {
            return _context.AspNetUsers.ToList();
        }

        public AspNetUsers GetUser(int id)
        {
            return _context.AspNetUsers.FirstOrDefault(u => u.Id == id);
        }

        public AspNetUsers RetrieveUserByUsername(string username)
        {
            return _context.AspNetUsers.FirstOrDefault(u => u.UserName == username);
        }

        public AspNetUsers RetrieveUserByEmailAddress(string emailAddress)
        {
            return _context.AspNetUsers.FirstOrDefault(u => u.Email == emailAddress);
        }

        public void UpdateUser(AspNetUsers user)
        {
            var existing = GetUser(user.Id);
            _mapper.Map(user, existing);
            _context.SaveChanges();
        }

        public void DeleteUser(AspNetUsers user)
        {
            var existing = GetUser(user.Id);
            _context.AspNetUsers.Remove(existing);
            _context.SaveChanges();
        }
    }
}