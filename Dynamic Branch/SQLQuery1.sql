select * from ItemsRateSecondary
select * from itemsrateMaster
select * from items

select distinct itemid,issueheadname,rate from ItemsRateSecondary

select * from ItemsRateSecondary where itemid=1 and IssueHeadName='works'
order by orderno

select itemid,issueheadname,Rate,OrderNo,Quantity from ItemsRateSecondary IT2 
where itemid=1  and IssueHeadName='works' order by rate,OrderNo 

select * from DeliveryItemsDetails where DeliveryItemsChallanID=100181


select itemid,issueheadname,Rate,OrderNo,Quantity,Rate*Quantity as AMT from ItemsRateSecondary IT2  where itemid=1 and issueheadname='works' and quantity>0 order by rate,OrderNo

