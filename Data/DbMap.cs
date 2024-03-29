﻿/*
 * This class contains columns of database tables
 * (for example, columns of Employee table in database)
 */

namespace supermarket.Data
{
    class DbMaps
    {
        public enum StoreProductMap : int
        {
            UPC,
            UPC_prom,
            id_product,
            selling_price,
            products_number,
            promotional_product,
            product_name,
        }

        public enum CustomerMap : int
        {
            card_number,
            cust_surname,
            cust_name,
            cust_patronymic,
            phone_number,
            city,
            street,
            zip_code,
            percent,
        }

        public enum EmployeeMap : int
        {
            id,
            surname,
            name,
            patronymic,
            role,
            salary,
            date_of_birth,
            date_of_start,
            phone_number,
            password,
            city,
            street,
            zipcode
        }

        public enum ProductMap : int
        {
            id_product,
            category_number,
            product_name,
            characteristics,
        }

        public enum CategoryMap : int
        {
            category_number,
            category_name,
        }

        public enum CheckMap : int
        {
            id_check,
            date,
            sum,
            pdv,
        }
    }
}