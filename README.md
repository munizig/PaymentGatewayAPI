## PaymentGatewayAPI
<a href="https://ci.appveyor.com/project/munizig/paymentgatewayapi">
  <img src="https://ci.appveyor.com/api/projects/status/81n68i2vesd71doj/branch/master?svg=true" />
</a> 

# API para Gateway de Pagamentos

A API é capaz de enviar requisições de compras, por exemplo, cartão de crédito, para empresas processadoras de pagamentos, as chamadas adquirentes, e oferece ao lojista um único ponto de integração para várias adquirentes. A vantagem de se usar um gateway é que com apenas um contrato de integração o lojista poderá se integrar com várias empresas de pagamentos e com sistemas antifraudes.

O gateway especializado em e-commerce e pode processar pagamentos de vários lojistas. Cada lojista poderá ter contrato com mais de um adquirente e ainda poderá ter ou não contrato com sistemas antifraudes.


# Processo de Pagamento

A API se integra com dois adquirentes famosos no mercado brasileiro.

Cada loja (cliente) tem configurações cadastradas em Banco de Dados para informar com quais adquirentes têm contrato. As configurações também definem em qual adquirente cada cartão de crédito será utilizado nas transações (pagamentos).

