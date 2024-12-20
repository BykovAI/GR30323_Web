using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GR30323_Web.Domain.Entities
{
    public class Car
    {
        public int Id { get; set; } // Уникальный идентификатор
        public string Name { get; set; } // Название автомобиля
        public string Description { get; set; } // Описание автомобиля
        public string? Image { get; set; } // Путь к изображению
        public int CategoryId { get; set; } // Идентификатор категории
        
        [JsonIgnore]  // Игнорируем связь с категорией при сериал
        public Category? Category { get; set; } // Связь с категорией
        public decimal Price { get; set; } // Цена автомобиля

    }

}
