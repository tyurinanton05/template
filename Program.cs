using System;
using System.Text;
using System.Collections.Generic;

namespace Prtn_Iterator__Visitor
{
    /// <summary>
    /// Главный класс программы
    /// </summary>
    class Program
    {
        /// <summary>
        /// Запускающий метод
        /// </summary>
        /// <param name="args">Аргументы</param>
        static void Main(string[] args)
        {
            // Список товаров
            List<Goods.Good> goods = new List<Goods.Good>();
            // Добавляем товары в список
            goods.Add(new Goods.Computer("PC basic configuration", 400));
            goods.Add(new Goods.Notebook("Notebook Asus pro", 450));
            goods.Add(new Goods.Notebook("Notebook Samsung", 560));
            goods.Add(new Goods.Notebook("Notebook Sony Vaio", 620));
            goods.Add(new Goods.Computer("PC gamer configuration", 990));

            // Создаем итерабельную коллекцию
            var good_list = new Iterators.Nomenclature(goods);

            // Вывод начального прайса на консоль
            PrintPriceList(goods);

            // Создаем скидки на ноутбуки - 5%
            var discounter = new Visitor.Discounter(5);
            // Итератор перебора всех ноутбуков
            Iterators.Iterator notebooksIt = 
                good_list.CreateIterator(new object[] { typeof(Goods.Notebook) });
            // Изменение цены ноутбуков
            while (notebooksIt.HasNext())
                ((Goods.Notebook)notebooksIt.Next()).ModifyPrice(discounter);

            // Создаем наценки на настольные компьютеры - 3% + 5$
            var increaser = new Visitor.Increaser(3, 5);
            // Итератор перебора всех настольных компьютеров
            Iterators.Iterator desktopsIt =
                good_list.CreateIterator(new object[] { typeof(Goods.Computer) });
            // Изменение цены настольных компьютеров
            while (desktopsIt.HasNext())
                ((Goods.Computer)desktopsIt.Next()).ModifyPrice(increaser);

            // Вывод конечного прайса на консоль
            PrintPriceList(goods);

            // Ожидание действия пользователя
            Console.ReadKey();
        }


        /// <summary>
        /// Печать списка товаров с ценами
        /// </summary>
        /// <param name="goods">Список товаров</param>
        private static void PrintPriceList(List<Goods.Good> goods)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("-----------------------------------------\n")
                         .AppendFormat("  PRICE LIST\n")
                         .AppendFormat("-----------------------------------------\n");
            foreach (var item in goods)
                stringBuilder.AppendFormat("{0}\n", item.ToString());
            stringBuilder.AppendFormat("-----------------------------------------");
            Console.WriteLine("\n");
            Console.WriteLine(stringBuilder.ToString());
            Console.WriteLine("\n");
        }
    }
}