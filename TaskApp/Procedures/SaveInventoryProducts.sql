--Procedura wstawiająca InventoryProduct do bazy danych

create procedure save_inventory_products(IN productid text, IN unit text, IN quantity real, IN shippingtime text, IN shippingcost real)
    language plpgsql
as
$$
begin
    insert into "InventoryProducts"("ProductId", "Unit", "Quantity", "ShippingTime", "ShippingCost")
    values(productId, unit, quantity, shippingTime, shippingCost);
end
$$;

alter procedure save_inventory_products(text, text, real, text, real) owner to postgres;