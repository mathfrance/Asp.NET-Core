
class Carrinho {
    clickIncremento(btn) {
        let data = this.getData(btn);
        data.Quantidade++;
        this.postQuantidade(data);
    }

    clickDecremento(btn) {
        let data = this.getData(btn);
        data.Quantidade--;
        this.postQuantidade(data);
    }

    updateQuantidade(input) {
        let data = this.getData(input);
        this.postQuantidade(data);
    }

    getData(elemento) {
        var linhaDoitem = $(elemento).parents('[item-id]');
        var itemId = $(linhaDoitem).attr('item-id');
        var novaQuantidade = $(linhaDoitem).find('input').val();

        return {
            Id: itemId,
            Quantidade: novaQuantidade
        };
    }

    postQuantidade(data) {
        $.ajax({
            url: '/pedido/updatequantidade',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).done(function (response) {
            location.reload();
            //let itemPedido = response.itemPedido;
            //let linhaDoItem = $('[item-id=' + itemPedido.Id + ']')
            //linhaDoItem.find('input').val(itemPedido.Quantidade);
        });
    }
}

var carrinho = new Carrinho();


