// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// handle tab switching
var dataTabs = new bootstrap.Tab(document.getElementById('producers-tab'));
dataTabs.show();

// handle searching
$(document).ready(function () {
    $('#producersSearch').on('input', function () {
        filterProducers($('#producersSearch').val());
    });

    $('#countryFilter').on('change', function () {
        filterProducers($('#producersSearch').val());
    });

    function filterProducers(searchTerm) {
        var selectedCountry = $('#countryFilter').val() || [];
        $('#producersTable tbody tr').each(function () {
            var rowText = $(this).find('td:eq(1)').text().toLowerCase();
            var rowCountry = $(this).find('.countryData').text().toLowerCase();

            var matchesSearchTerm = rowText.includes(searchTerm.toLowerCase());
            var matchesCountryFilter = selectedCountry.length === 0 || selectedCountry.toLowerCase() === rowCountry;

            if (matchesSearchTerm && matchesCountryFilter) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
    }

    $('#maxWeightFilter, #minPowerFilter, #speakersSearch, #colorFilter').on('input', function () {
        var maxWeight = parseFloat($('#maxWeightFilter').val()) || Number.POSITIVE_INFINITY;
        var minPower = parseFloat($('#minPowerFilter').val()) || Number.NEGATIVE_INFINITY;
        filterSpeakers($('#speakersSearch').val(), $('#colorFilter').val(), maxWeight, minPower);
    });  

    function filterSpeakers(searchTerm, selectedColor, maxWeight, minPower) {
        $('#speakersTable tbody tr').each(function () {
            var rowText = $(this).find('td:eq(1)').text().toLowerCase();
            var rowColor = $(this).find('td:eq(5)').text().toLowerCase();
            var rowWeight = parseFloat($(this).find('td:eq(4)').text().replace(',', '.')) || 0;
            var rowPower = parseFloat($(this).find('td:eq(3)').text().replace(',', '.')) || 0;

            var matchesSearchTerm = rowText.includes(searchTerm.toLowerCase());
            var matchesColorFilter = selectedColor.length === 0 || selectedColor.toLowerCase() === rowColor;
            var matchesMaxWeight = rowWeight <= maxWeight;
            var matchesMinPower = rowPower >= minPower;

            if (matchesSearchTerm && matchesColorFilter && matchesMaxWeight && matchesMinPower) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
    }

    
});