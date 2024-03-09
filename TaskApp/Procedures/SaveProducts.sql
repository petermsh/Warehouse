--Procedura wstawiająca Product do bazy danych

create procedure save_products(IN id text, IN sku text, IN name text, IN ean text, IN producername text, IN category text, IN iswire boolean, IN defaultimage text, IN shipping text)
    language plpgsql
as
$$
begin
    insert into "Products"("Id", "Sku", "Name", "Ean", "ProducerName", "Category", "IsWire", "DefaultImage", "Shipping")
    values(id, sku, name, ean, producername, category, iswire, defaultimage, shipping);
end
$$;

alter procedure save_products(text, text, text, text, text, text, boolean, text, text) owner to postgres;