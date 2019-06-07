## PaymentGatewayAPI
<a href="https://ci.appveyor.com/project/munizig/paymentgatewayapi">
  <img src="https://ci.appveyor.com/api/projects/status/81n68i2vesd71doj/branch/master?svg=true" />
</a> 

# Payment Gateway's API

The API is able to send purchase requisitions to payment processing companies (the buyer calls) and offers the merchant a single point of integration for multiple buyers. The advantage of using a gateway is that with just one integration contract the tenant can integrate with several payment companies and with various anti-fraud systems in an easy and practical way.

The gateway specializes in e-commerce and can process payments from multiple tenants. Each tenant may have a contract with more than one buyer and may or may not have an anti-fraud agreement.


# Payment Process

The API integrates with two famous acquirers in the Brazilian market.

Each store (customer) has configurations registered in Database to inform with which buyers have contract. The settings also define in which acquirer each credit card will be used in transactions (payments).
