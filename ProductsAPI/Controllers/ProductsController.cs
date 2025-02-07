﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsAPI.DTOs;
using ProductsAPI.Entities;
using ProductsAPI.Repositories;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// Serviço para cadastro de produto da API.
        /// </summary>
        [HttpPost]
        public IActionResult Post([FromBody] ProductRequestDto request)
        {
            //criando um objeto da classe Produto
            var product = new Product
            {
                Id = Guid.NewGuid(), //gerando o ID
                Name = request.Name, //capturando o nome do produto
                Price = request.Price, //capturando o preço do produto
                Quantity = request.Quantity, //capturando a quantidade do produto
                CategoryId = request.CategoryId //capturando o id da categoria
            };

            //criando um objeto da classe de repositório
            var productRepository = new ProductRepository();
            productRepository.Add(product); //gravando o produto no banco de dados

            //retornar um status de sucesso na API (HTTP 200 - OK)
            return Ok(new 
            {
                Message = "Produto cadastrado com sucesso.", //mensagem de sucesso
                CreatedAt = DateTime.Now, //data e hora do cadastro
                ProductId = product.Id, //ID do produto que foi cadastrado
            });
        }

        /// <summary>
        /// Serviço para atualização de produto na API.
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ProductRequestDto request)
        {
            //criando um objeto para a classe do repositório
            var productRepository = new ProductRepository();
            //consultar o produto no banco de dados através do ID
            var product = productRepository.GetById(id);

            //modificando os dados do produto com as informações da requisição
            product.Name = request.Name;
            product.Price = request.Price;
            product.Quantity = request.Quantity;
            product.CategoryId = request.CategoryId;

            //atualizar o produto no banco de dados
            productRepository.Update(product);

            //retornando os dados da resposta
            return Ok(new
            {
                Message = "Produto atualizado com sucesso.", //mensagem de sucesso
                UpdatedAt = DateTime.Now, //data e hora da atualização
                ProductId = id, //id do produto que foi atualizado
            });
        }

        /// <summary>
        /// Serviço para exclusão de produto na API
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            //criando um objeto para a classe do repositório
            var productRepository = new ProductRepository();
            //consultar o produto no banco de dados através do ID
            var product = productRepository.GetById(id);

            //excluindo o produto
            productRepository.Delete(product);

            //retornando os dados da resposta
            return Ok(new 
            {
                Message = "Produto excluído com sucesso.", //mensagem de sucesso
                DeletedAt = DateTime.Now, //data e hora da exclusão
                ProductId = id, //id do produto que foi excluido
            });
        }

        /// <summary>
        /// Serviço para consulta de produtos na API.
        /// </summary>
        [HttpGet]
        public IActionResult Get()
        {
            //criando um objeto da classe ProductRepository
            var productRepository = new ProductRepository();
            //consultar todos os produtos da base de dados
            var products = productRepository.GetAll();

            //criando uma lista da classe DTO
            var response = new List<ProductResponseDto>();

            //percorrendo cada produto obtido do banco de dados
            foreach (var product in products)
            {
                response.Add(new ProductResponseDto 
                { 
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    CategoriaId = product.CategoryId,
                });
            }

            //retornar os dados
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            //criando um objeto para a classe do repositório
            var productRepository = new ProductRepository();
            //consultar o produto no banco de dados através do ID
            var product = productRepository.GetById(id);

            //criando um objeto da classe DTO para retornar os dados do produto
            var response = new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoriaId = product.CategoryId,
            };

            //retornando os dados do dto
            return Ok(response);
        }
    }
}
