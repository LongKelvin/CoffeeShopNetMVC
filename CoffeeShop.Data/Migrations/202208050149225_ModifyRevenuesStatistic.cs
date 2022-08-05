namespace CoffeeShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ModifyRevenuesStatistic : DbMigration
    {
        public override void Up()
        {
            AlterStoredProcedure("GetRevenueStatistic",
              p => new
              {
                  fromDate = p.String(),
                  toDate = p.String()
              }
              ,
              @"
               SELECT CONVERT(VARCHAR(10), o.CreatedDate, 111) AS Date,
                   sum(od.Quantity*od.UnitPrice) AS Revenues,
                   sum((od.Quantity*od.UnitPrice)-(od.Quantity*p.OriginalPrice)) AS Benefit
               FROM Orders o
               INNER JOIN OrderDetails od ON o.ID = od.OrderId
               INNER JOIN Products p ON od.ProductID = p.ID
               WHERE o.CreatedDate <= cast(@toDate AS date)
                  AND o.CreatedDate >= cast(@fromDate AS date)
               GROUP BY CONVERT(varchar(10), o.CreatedDate, 111)
               ORDER BY CONVERT(varchar(10), o.CreatedDate, 111)"
              );
        }

        public override void Down()
        {
            DropStoredProcedure("dbo.GetRevenueStatistic");
        }
    }
}