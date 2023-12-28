Select proID, pName,
                        (Select SUM(qty) from tblDetails d inner join tblMain m on m.MainID=d.dMainID where m.mType ='PUR' and d.productID=proID)
                       - (Select SUM(qty) from tblDetails d inner join tblMain m on m.MainID=d.dMainID where m.mType ='SAL' and d.productID=proID)
                        from Product;


