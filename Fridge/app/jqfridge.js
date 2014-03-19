'use strict';

$(function () {
    $('input').addClass('form-control');

    $.get('/api/Food').then(function (foods) {
        var $tbody = $('#foods-table >  tbody:last');

        // OOPS - spot the typo?
        //$.each(foods, function (food) {
        //    $tbody.append('<tr><td>' + food.Brand + '</td></tr>');
        //});

        $.each(foods, function (i, food) {
            var $tr = $('<tr>' +
                '<td>' +
                '<a class="edit-item btn btn-primary btn-xs" data-foodid="' + food.Id + '">Edit</a>&nbsp;' +
                '<a class="delete-item btn btn-danger btn-xs" data-foodid="' + food.Id + '">Delete</a>' +
                '</td>' +
                '<td>' + food.Kind + '</td>' +
                '<td>' + food.Brand + '</td>' +
                '<td>' + food.Name + '</td>' +
                '<td>' + food.PortionSize + '</td>' +
                '<td>' + food.PortionUnits + '</td>' +
                '</tr>');
            $tr.data('food', food);
            $tbody.append($tr);
        });

        $('.delete-item').on('click', function (event) {
            event.preventDefault();
            var $target = $(event.target);
            $.ajax({
                url: '/api/Food/' + $target.attr('data-foodid'),
                type: 'DELETE'
            }).then(function () {
                alert('food deleted');
            });
        });

        $('.edit-item').on('click', function(event) {
            event.preventDefault();
            var $target = $(event.target);
            var targetFood = $target.parents('tr').data('food');
            $('#Id').val(targetFood.Id);
            $('#Kind').val(targetFood.Kind);
            $('#Brand').val(targetFood.Brand);
            $('#Name').val(targetFood.Name);
            $('#PortionSize').val(targetFood.PortionSize);
            $('#PortionUnits').val(targetFood.PortionUnits);
        });
    });

    $('#submitFood').on('click', function (event) {
        event.preventDefault();
        var id = $('#Id').val();
        var food = {
            'Brand': $('#Brand').val(),
            'Name': $('#Name').val(),
            'Kind': $('#Kind').val(),
            'PortionUnits': $('#PortionUnits').val(),
            'PortionSize': $('#PortionSize').val()
        };
        if (id > 0) {
            food.Id = id;
            $.ajax({
                url: '/api/Food/' + id,
                type: 'PUT',
                data: food
            }).then(function() {
                alert('food updated');
            });
        } else {
            $.post('/api/Food', food).then(function() {
                alert('food created');
            });
        }
    });

});