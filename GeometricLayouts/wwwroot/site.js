const uri = 'api/triangle';

$(document).ready(function () {
});

function getTriangleFromPoints() {

    var points = [];
    points.push($('#xCoord1').val() + "," + $('#yCoord1').val());
    points.push($('#xCoord2').val() + "," + $('#yCoord2').val());
    points.push($('#xCoord3').val() + "," + $('#yCoord3').val());
    
    console.log('"' + JSON.stringify(points) + '"');

    $.ajax({
        type: 'POST',
        url: uri,
        contentType: 'application/json',
        data: JSON.stringify(points),
        error: function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status === 404) {
                document.getElementById('triangleNotFoundByPoints').innerHTML = "No triangle contained all three points provided, please try again.";
                clearDownValues();
            }
        },
        success: function (result) {
            displayMatchedTriangle(result);
            draw(result.points);
            $('#xCoord1').val('');
            $('#yCoord1').val('');
            $('#xCoord2').val('');
            $('#yCoord2').val('');
            $('#xCoord3').val('');
            $('#yCoord3').val('');
            document.getElementById('triangleNotFoundByPoints').innerHTML = "";
        }
    });
}

function clearDownValues() {
    $('#row').val('')
    $('#column').val('')
    $('#xCoord1').val('');
    $('#yCoord1').val('');
    $('#xCoord2').val('');
    $('#yCoord2').val('');
    $('#xCoord3').val('');
    $('#yCoord3').val('');

    document.getElementById('triangleDescription').innerHTML = "";

    var canvas = document.getElementById('triangleCanvas');
    if (canvas.getContext) {
        var ctx = canvas.getContext('2d');
        ctx.clearRect(0, 0, canvas.width, canvas.height);
    }
}

function displayMatchedTriangle(triangle) {
    var rowAsChar = String.fromCharCode(65 + triangle.row); 

    document.getElementById('triangleDescription').innerHTML = "Triangle: " + rowAsChar + triangle.column;
} 

function drawTriangle() {
    var rowValueString = $('#row').val().toUpperCase();
    var rowIndex = rowValueString.charCodeAt(0) - 65;
    
    $.ajax({
        type: 'GET',
        url: uri + '?row=' + rowIndex + '&column=' + $('#column').val(),
        error: function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status === 404){
                document.getElementById('triangleNotFoundByRowAndColumn').innerHTML = "A triangle was not found using the given Row and Column, please follow the row and column description above";
                clearDownValues();
            }
        },
        success: function (result) {
            draw(result.points);
            displayMatchedTriangle(result);
            $('#row').val('');
            $('#column').val('');
            document.getElementById('triangleNotFoundByRowAndColumn').innerHTML = "";
        }
    });
}

function draw(points) {
    var canvas = document.getElementById('triangleCanvas');
    if (canvas.getContext) {
        var ctx = canvas.getContext('2d');
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        ctx.beginPath();
        var coords = points[0].split(",");
        ctx.moveTo(coords[0] * 10, coords[1] * 10);
        points.splice(points.indexOf(points[0]), 1);
        
        points.forEach(function (point) {
            var c = point.split(",");
            ctx.lineTo(c[0] * 10, c[1] * 10);
        });

        ctx.fill();
    }
}