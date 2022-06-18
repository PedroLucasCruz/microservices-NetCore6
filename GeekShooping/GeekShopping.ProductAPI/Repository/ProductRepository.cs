using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;
using GeekShopping.ProductAPI.Model.Context;
using GeekShopping.ProductAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly SqlContext _sQLContext;
        private IMapper _mapper;

        public ProductRepository(SqlContext sQLContext, IMapper mapper)
        {
            _sQLContext = sQLContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
           List<Product> products = await _sQLContext.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> FindById(long id)
        {
            //integorrogação para permitir o retorno de nullos também
            Product? products = await _sQLContext.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<ProductVO>(products);
        }
        public async Task<ProductVO> Create(ProductVO vo)
        {
            Product products = _mapper.Map<Product>(vo);
            _sQLContext.Products.Add(products);
            await _sQLContext.SaveChangesAsync();
            return _mapper.Map<ProductVO>(products);
        }

        public async Task<ProductVO> Update(ProductVO vo)
        {
            Product products = _mapper.Map<Product>(vo);
            _sQLContext.Products.Update(products);
            await _sQLContext.SaveChangesAsync();
            return _mapper.Map<ProductVO>(products);
        }
               
        public async Task<bool> Delete(long id)
        {
            try
            {
                Product? products = await _sQLContext.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
                if (products == null) return false;
                _sQLContext.Products.Remove(products);
                await _sQLContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

       
    }
}
