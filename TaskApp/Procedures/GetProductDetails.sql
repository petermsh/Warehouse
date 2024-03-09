--Procedura pobierająca informacje o produkcie na podstawie SKU zapisana w bazie danych

create function get_product_details(in_sku text)
    returns TABLE(productname text, ean text, producername text, category text, image text, quantity real, unit text, netproductcost real, shippingcost real)
    language plpgsql
as
$$
BEGIN
RETURN QUERY
SELECT
    p."Name" AS productName,
    p."Ean" AS ean,
    p."ProducerName" AS producerName,
    p."Category" AS category,
    p."DefaultImage" AS image,
    ip."Quantity" AS quantity,
    ip."Unit" AS unit,
    pr."UnitPriceWithDiscount" AS netProductCost,
    ip."ShippingCost" AS shippingCost
FROM
    public."Products" p
        JOIN
    public."InventoryProducts" ip ON p."Id" = ip."ProductId"
        JOIN
    public."Prices" pr ON p."Sku" = pr."Sku"
WHERE
        p."Sku" = in_sku;
END;
$$;

alter function get_product_details(text) owner to postgres;