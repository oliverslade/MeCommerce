using AutoMapper;
using DataModels;
using Interfaces.Repositories;
using System.Linq;

namespace Repository
{
    public class ShoppingBasketRepository : IShoppingBasketRepository
    {
        private readonly MeCommerceDbContext _context = new MeCommerceDbContext();

        private readonly IMapper _mapper;

        public ShoppingBasketRepository()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ShoppingCart, ShoppingCart>()
                    .ForMember(m => m.CartId, opt => opt.Ignore());

                cfg.CreateMap<ShoppingCartItem, ShoppingCartItem>()
                    .ForMember(m => m.ShoppingCartItemsId, opt => opt.Ignore());
            }).CreateMapper();
        }

        public ShoppingCart GetUserCart(int userId)
        {
            return _context.ShoppingCart.FirstOrDefault(x => x.User.Id == userId);
        }

        public void CreateBasket(ShoppingCart cart)
        {
            _context.ShoppingCart.Add(cart);
            _context.SaveChanges();
        }

        public void UpdateBasket(ShoppingCart cart)
        {
            var existing = GetUserCart(cart.User.Id);
            _mapper.Map(cart, existing);
            _context.SaveChanges();
        }

        public void DeleteShoppingCartItem(int productId)
        {
            var existing = _context.ShoppingCartItem.FirstOrDefault(x => x.ProductId == productId);
            _context.ShoppingCartItem.Remove(existing);
            _context.SaveChanges();
        }

        public void DeleteCartByUserId(int userId)
        {
            var existing = _context.ShoppingCart.FirstOrDefault(x => x.User.Id == userId);
            _context.ShoppingCart.Remove(existing);
            _context.SaveChanges();
        }
    }
}