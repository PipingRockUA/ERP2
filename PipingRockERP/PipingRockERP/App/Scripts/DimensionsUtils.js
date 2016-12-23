function handleDimensionsUpdate($inputXI, $inputYI, $inputZI, $inputResultI, $inputXC, $inputYC, $inputZC, $inputResultC) {
    var calculatedDimensions = 0;
    var result = 0;
    var inputX,
        inputY,
        inputZ;

    $inputXI.on("change", function (event) {
        toCm($inputXI, $inputXC);
        recalculateDimensionsI();
        recalculateDimensionsC();
    });
    $inputYI.on("change", function (event) {
        toCm($inputYI, $inputYC);
        recalculateDimensionsI();
        recalculateDimensionsC();
    });
    $inputZI.on("change", function (event) {
        toCm($inputZI, $inputZC);
        recalculateDimensionsI();
        recalculateDimensionsC();
    });
    $inputXC.on("change", function (event) {
        toInches($inputXI, $inputXC);
        recalculateDimensionsC();
        recalculateDimensionsI();
    });
    $inputYC.on("change", function (event) {
        toInches($inputYI, $inputYC);
        recalculateDimensionsC();
        recalculateDimensionsI();
    });
    $inputZC.on("change", function (event) {
        toInches($inputZI, $inputZC);
        recalculateDimensionsC();
        recalculateDimensionsI();
    });

    function recalculateDimensionsI() {
        inputX = parseFloat($inputXI.val().replace(",", "."));
        inputY = parseFloat($inputYI.val().replace(",", "."));
        inputZ = parseFloat($inputZI.val().replace(",", "."));

        calculatedDimensions = inputX * inputY * inputZ;
        $inputResultI.val(Math.round(calculatedDimensions*100)/100);
    }
    function recalculateDimensionsC() {
        inputX = parseFloat($inputXC.val().replace(",", "."));
        inputY = parseFloat($inputYC.val().replace(",", "."));
        inputZ = parseFloat($inputZC.val().replace(",", "."));

        calculatedDimensions = inputX * inputY * inputZ;
        $inputResultC.val(Math.round(calculatedDimensions * 100) / 100);
    }
    function toInches($inputInches, $inputCm) {
        result = parseFloat($inputCm.val().replace(",", "."));
        $inputInches.val(Math.round(result / 2.54 * 100) / 100);
    }
    function toCm($inputInches, $inputCm) {
        result = parseFloat($inputInches.val().replace(",", "."));
        $inputCm.val(Math.round(result * 2.54 * 100) / 100);
    }
}

function handleDimensionsInput($inputInches, $inputCm) {
    var result = 0;

    $inputInches.on("change", function (event) {
        result = parseFloat($inputInches.val().replace(",", "."));
        $inputCm.val(Math.round(result * 2.54 * 100) / 100);
    });
    $inputCm.on("change", function (event) {
        result = parseFloat($inputInches.val().replace(",", "."));
        $inputInches.val(Math.round(result / 2.54 * 100) / 100);
    });
}