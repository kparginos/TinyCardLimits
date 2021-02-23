# TinyCardLimits

This is a test project, made for learning purposes.

Solution contains 4 projects:
-----------------------------

1. TinyCardLimits.Migrations under app
2. TinyCardLimits.Api under app
3. TinyCardLimits.Core under src
4. TinyCardLimits.Core.Test under test

Web.Api availiable end points:
------------------------------

1. Authorization endpoint:
   Call method: POST
   Sample URL: https://localhost:5001/CardLimit/authorize
   Sample Body message(JSON):
   {
     "CardNumber" : "some card number here",
     "TransactionType" : 1,
     "TransAmount" : 1300.00
   }
   At "TransactionType" set 1 for for CardPresent and 2 for eCommerce

2. Card Limit daily aggregation amount:
   Call method: GET
   Sample URL: https://localhost:5001/CardLimit/some_card_number_here

3. Health check end point:
   Call method: GET
   Sample URL: https://localhost:5001/home
