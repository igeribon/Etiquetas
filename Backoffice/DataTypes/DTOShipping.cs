﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DataTypes
{

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class ClientDetails
        {
            public string accept_language { get; set; }
            public int browser_height { get; set; }
            public string browser_ip { get; set; }
            public int browser_width { get; set; }
            public object session_hash { get; set; }
            public string user_agent { get; set; }
        }

        public class ShopMoney
        {
            public string amount { get; set; }
            public string currency_code { get; set; }
        }

        public class PresentmentMoney
        {
            public string amount { get; set; }
            public string currency_code { get; set; }
        }

        public class PriceSet
        {
            public ShopMoney shop_money { get; set; }
            public PresentmentMoney presentment_money { get; set; }
        }

        public class TaxLine
        {
            public string price { get; set; }
            public double rate { get; set; }
            public string title { get; set; }
            public PriceSet price_set { get; set; }
        }

        public class TotalLineItemsPriceSet
        {
            public ShopMoney shop_money { get; set; }
            public PresentmentMoney presentment_money { get; set; }
        }

        public class TotalDiscountsSet
        {
            public ShopMoney shop_money { get; set; }
            public PresentmentMoney presentment_money { get; set; }
        }

        public class TotalShippingPriceSet
        {
            public ShopMoney shop_money { get; set; }
            public PresentmentMoney presentment_money { get; set; }
        }

        public class SubtotalPriceSet
        {
            public ShopMoney shop_money { get; set; }
            public PresentmentMoney presentment_money { get; set; }
        }

        public class TotalPriceSet
        {
            public ShopMoney shop_money { get; set; }
            public PresentmentMoney presentment_money { get; set; }
        }

        public class TotalTaxSet
        {
            public ShopMoney shop_money { get; set; }
            public PresentmentMoney presentment_money { get; set; }
        }

        public class TotalDiscountSet
        {
            public ShopMoney shop_money { get; set; }
            public PresentmentMoney presentment_money { get; set; }
        }

        public class OriginLocation
        {
            public long id { get; set; }
            public string country_code { get; set; }
            public string province_code { get; set; }
            public string name { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string city { get; set; }
            public string zip { get; set; }
        }

        public class LineItem
        {
            public long id { get; set; }
            public long variant_id { get; set; }
            public string title { get; set; }
            public int quantity { get; set; }
            public string sku { get; set; }
            public string variant_title { get; set; }
            public string vendor { get; set; }
            public string fulfillment_service { get; set; }
            public long product_id { get; set; }
            public bool requires_shipping { get; set; }
            public bool taxable { get; set; }
            public bool gift_card { get; set; }
            public string name { get; set; }
            public string variant_inventory_management { get; set; }
            public List<object> properties { get; set; }
            public bool product_exists { get; set; }
            public int fulfillable_quantity { get; set; }
            public int grams { get; set; }
            public string price { get; set; }
            public string total_discount { get; set; }
            public string fulfillment_status { get; set; }
            public PriceSet price_set { get; set; }
            public TotalDiscountSet total_discount_set { get; set; }
            public List<object> discount_allocations { get; set; }
            public List<object> duties { get; set; }
            public string admin_graphql_api_id { get; set; }
            public List<TaxLine> tax_lines { get; set; }
            public OriginLocation origin_location { get; set; }
        }

        public class Receipt
        {
        }

        public class Fulfillment
        {
            public long id { get; set; }
            public long order_id { get; set; }
            public string status { get; set; }
            public DateTime created_at { get; set; }
            public string service { get; set; }
            public DateTime updated_at { get; set; }
            public string tracking_company { get; set; }
            public object shipment_status { get; set; }
            public long location_id { get; set; }
            public List<LineItem> line_items { get; set; }
            public string tracking_number { get; set; }
            public List<string> tracking_numbers { get; set; }
            public string tracking_url { get; set; }
            public List<string> tracking_urls { get; set; }
            public Receipt receipt { get; set; }
            public string name { get; set; }
            public string admin_graphql_api_id { get; set; }
        }

        public class DiscountedPriceSet
        {
            public ShopMoney shop_money { get; set; }
            public PresentmentMoney presentment_money { get; set; }
        }

        public class ShippingLine
        {
            public long id { get; set; }
            public string title { get; set; }
            public string price { get; set; }
            public string code { get; set; }
            public string source { get; set; }
            public object phone { get; set; }
            public object requested_fulfillment_service_id { get; set; }
            public object delivery_category { get; set; }
            public object carrier_identifier { get; set; }
            public string discounted_price { get; set; }
            public PriceSet price_set { get; set; }
            public DiscountedPriceSet discounted_price_set { get; set; }
            public List<object> discount_allocations { get; set; }
            public List<object> tax_lines { get; set; }
        }

        public class BillingAddress
        {
            public string first_name { get; set; }
            public string address1 { get; set; }
            public string phone { get; set; }
            public string city { get; set; }
            public string zip { get; set; }
            public object province { get; set; }
            public string country { get; set; }
            public string last_name { get; set; }
            public object address2 { get; set; }
            public object company { get; set; }
            public object latitude { get; set; }
            public object longitude { get; set; }
            public string name { get; set; }
            public string country_code { get; set; }
            public object province_code { get; set; }
        }

        public class ShippingAddress
        {
            public string first_name { get; set; }
            public string address1 { get; set; }
            public string phone { get; set; }
            public string city { get; set; }
            public string zip { get; set; }
            public object province { get; set; }
            public string country { get; set; }
            public string last_name { get; set; }
            public object address2 { get; set; }
            public object company { get; set; }
            public object latitude { get; set; }
            public object longitude { get; set; }
            public string name { get; set; }
            public string country_code { get; set; }
            public object province_code { get; set; }
        }

        public class DefaultAddress
        {
            public long id { get; set; }
            public long customer_id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public object company { get; set; }
            public string address1 { get; set; }
            public object address2 { get; set; }
            public string city { get; set; }
            public object province { get; set; }
            public string country { get; set; }
            public string zip { get; set; }
            public string phone { get; set; }
            public string name { get; set; }
            public object province_code { get; set; }
            public string country_code { get; set; }
            public string country_name { get; set; }
            public bool @default { get; set; }
        }

        public class Customer
        {
            public long id { get; set; }
            public object email { get; set; }
            public bool accepts_marketing { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public int orders_count { get; set; }
            public string state { get; set; }
            public string total_spent { get; set; }
            public long last_order_id { get; set; }
            public object note { get; set; }
            public bool verified_email { get; set; }
            public object multipass_identifier { get; set; }
            public bool tax_exempt { get; set; }
            public string phone { get; set; }
            public string tags { get; set; }
            public string last_order_name { get; set; }
            public string currency { get; set; }
            public DateTime accepts_marketing_updated_at { get; set; }
            public object marketing_opt_in_level { get; set; }
            public string admin_graphql_api_id { get; set; }
            public DefaultAddress default_address { get; set; }
        }

        public class DTOShipping
        {
            public long id { get; set; }
            public string email { get; set; }
            public object closed_at { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public int number { get; set; }
            public object note { get; set; }
            public string token { get; set; }
            public string gateway { get; set; }
            public bool test { get; set; }
            public string total_price { get; set; }
            public string subtotal_price { get; set; }
            public int total_weight { get; set; }
            public string total_tax { get; set; }
            public bool taxes_included { get; set; }
            public string currency { get; set; }
            public string financial_status { get; set; }
            public bool confirmed { get; set; }
            public string total_discounts { get; set; }
            public string total_line_items_price { get; set; }
            public string cart_token { get; set; }
            public bool buyer_accepts_marketing { get; set; }
            public string name { get; set; }
            public string referring_site { get; set; }
            public string landing_site { get; set; }
            public object cancelled_at { get; set; }
            public object cancel_reason { get; set; }
            public string total_price_usd { get; set; }
            public string checkout_token { get; set; }
            public object reference { get; set; }
            public object user_id { get; set; }
            public object location_id { get; set; }
            public object source_identifier { get; set; }
            public object source_url { get; set; }
            public DateTime processed_at { get; set; }
            public object device_id { get; set; }
            public string phone { get; set; }
            public string customer_locale { get; set; }
            public int app_id { get; set; }
            public string browser_ip { get; set; }
            public ClientDetails client_details { get; set; }
            public object landing_site_ref { get; set; }
            public int order_number { get; set; }
            public List<object> discount_applications { get; set; }
            public List<object> discount_codes { get; set; }
            public List<object> note_attributes { get; set; }
            public List<string> payment_gateway_names { get; set; }
            public string processing_method { get; set; }
            public long checkout_id { get; set; }
            public string source_name { get; set; }
            public string fulfillment_status { get; set; }
            public List<TaxLine> tax_lines { get; set; }
            public string tags { get; set; }
            public object contact_email { get; set; }
            public string order_status_url { get; set; }
            public string presentment_currency { get; set; }
            public TotalLineItemsPriceSet total_line_items_price_set { get; set; }
            public TotalDiscountsSet total_discounts_set { get; set; }
            public TotalShippingPriceSet total_shipping_price_set { get; set; }
            public SubtotalPriceSet subtotal_price_set { get; set; }
            public TotalPriceSet total_price_set { get; set; }
            public TotalTaxSet total_tax_set { get; set; }
            public List<LineItem> line_items { get; set; }
            public List<Fulfillment> fulfillments { get; set; }
            public List<object> refunds { get; set; }
            public string total_tip_received { get; set; }
            public object original_total_duties_set { get; set; }
            public object current_total_duties_set { get; set; }
            public string admin_graphql_api_id { get; set; }
            public List<ShippingLine> shipping_lines { get; set; }
            public BillingAddress billing_address { get; set; }
            public ShippingAddress shipping_address { get; set; }
            public Customer customer { get; set; }
        }

    }

