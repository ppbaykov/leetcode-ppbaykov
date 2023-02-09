select first.stock_name, sum(sum) as capital_gain_loss
from (select a.stock_name,
             a.operation_day     as buy_date,
             b.operation_day     as sell_date,
             (b.price - a.price) as sum
      from (select *
            from Stocks
            where operation = 'Buy') a,
           (select *
            from Stocks
            where operation = 'Sell') b
      where a.operation_day < b.operation_day
        and a.stock_name = b.stock_name) first

         inner join

     (select a.stock_name, a.operation_day as buy_date, min(b.operation_day) as sell_date
      from (select *
            from Stocks
            where operation = 'Buy') a,
           (select *
            from Stocks
            where operation = 'Sell') b
      where a.operation_day < b.operation_day
        and a.stock_name = b.stock_name
      group by a.stock_name, a.operation_day) second
     on first.stock_name = second.stock_name and
        first.buy_date = second.buy_date and
        first.sell_date = second.sell_date

group by first.stock_name