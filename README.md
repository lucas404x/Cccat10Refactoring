# Parte 1

## Objetivo

Vamos implementar um sistema de vendas online com a possibilidade de realizar pedidos com múltiplos itens, cada um deles com uma quantidade variável, calculando o frete, os impostos, aplicando um cupom de desconto e ainda interagindo com o estoque. Além disso teremos ainda fluxos de pagamento e cancelamento do pedido realizado.

## Requisitos

- [x] Deve criar um pedido com 3 produtos (com descrição, preço e quantidade) e calcular o valor total
- [x] Deve criar um pedido com 3 produtos, associar um cupom de desconto e calcular o total (percentual sobre o total do pedido)
- [x] Não deve criar um pedido com cpf inválido (lançar algum tipo de erro)
- [x] Refatorar o código do cpf para que ele fique mais legível e de fácil manutenção

### Considere

Utilizar e refatorar o algoritmo de validação de cpf: https://github.com/rodrigobranas/cccat7_refactoring/blob/master/src/example2/cpfBefore.ts

### Sugestões

- Faça a modelagem da forma que desejar e não se preocupe por enquanto, vamos - implementar juntos na aula seguinte com influências de DDD e Clean Architecture
- Utilize a sua linguagem e biblioteca de teste de sua preferência
- Devem existir no mínimo 2 arquivos, um de teste e outro que é a aplicação
- Como mecanismo de persistência você pode utilizar um banco de dados, um array em memória, um arquivo, qualquer coisa que desejar
- Como entrada você pode utilizar uma API HTTP, um CLI ou qualquer outro mecanismo que permita a entrada dos dados
- Tente seguir com disciplina, criando primeiro um teste que falha, depois fazendo e teste passar e refatorando

# Parte 2

### Testes

- [x] Não deve aplicar cupom de desconto expirado
- [x] Ao fazer um pedido, a quantidade de um item não pode ser negativa
- [x] Ao fazer um pedido, o mesmo item não pode ser informado mais de uma vez
- [x] Nenhuma dimensão do item pode ser negativa
- [x] O peso do item não pode ser negativo
- [x] Deve calcular o valor do frete com base nas dimensões (altura, largura e profundidade em cm) e o peso dos produtos (em kg)
- [x] Deve retornar o preço mínimo de frete caso ele seja superior ao valor calculado

### Considere

- O valor mínimo é de R$10,00
- Por enquanto, como não temos uma forma de calcular a distância entre o CEP de origem e destino, será de 1000 km (fixo)
- Utilize a fórmula abaixo para calcular o valor do frete

### Fórmula de Cálculo do Frete

Valor do Frete = distância (km) * volume (m3) * (densidade/100)

Exemplos de volume ocupado (cubagem)

Camera: 20cm x 15 cm x 10 cm = 0,003 m3
Guitarra: 100cm x 30cm x 10cm = 0,03 m3
Geladeira: 200cm x 100cm x 50cm = 1 m3

Exemplos de densidade

Camera: 1kg / 0,003 m3 = 333kg/m3
Guitarra: 3kg / 0,03 m3 = 100kg/m3
Geladeira: 40kg / 1 m3 = 40kg/m3

Exemplos

produto: Camera
distância: 1000 (fixo)
volume: 0,003
densidade: 333
preço: R$9,90 (1000 * 0,003 * (333/100))
preço mínimo: R$10,00

produto: Guitarra
distância: 1000 (fixo)
volume: 0,03
densidade: 100
preço: R$30,00 (1000 * 0,03 * (100/100))

produto: Geladeira
distância: 1000 (fixo)
volume: 1
densidade: 40
preço: R$400,00 (1000 * 1 * (40/100))

# Parte 3

### Testes

- [x] Deve gerar o número de série do pedido
- [x] Deve fazer um pedido, salvando no banco de dados
- [x] Deve simular o frete, retornando o frete previsto para o pedido
- [x] Deve validar o cupom de desconto, indicando em um boolean se o cupom é válido


### Considere

O número de série do pedido é formado por AAAAPPPPPPPP onde AAAA representa o ano e o PPPPPPPP representa um sequencial do pedido

### Importante

Implemente os DTOs para cada um dos use cases
Utilize o banco de dados para obter e persistir os dados
