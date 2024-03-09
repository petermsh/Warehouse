--Procedura wstawiająca Price do bazy danych

create procedure save_prices(IN id text, IN sku text, IN productprice real, IN productpricewithdisc real, IN unitpricewithdisc real, IN vatrate integer)
    language plpgsql
as
$$
begin
    insert into "Prices"("Id", "Sku", "ProductPrice", "ProductPriceWithDiscount", "UnitPriceWithDiscount", "VatRate")
    values(id, sku, productPrice, productPriceWithDisc, unitPriceWithDisc, vatrate);
end
$$;

alter procedure save_prices(text, text, real, real, real, integer) owner to postgres;