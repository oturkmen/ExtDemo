var Finance = function () { };
// Örnek: var sonuc = NPV(rate, array);
Finance.NPV = function (rate) {
    if (rate > 100) {
        rate = rate / 100;
    }
    var args = [];
    for (var i = 0; i < arguments.length; i++) {
        console.log('+++++++++++++');
        console.log('arguments[' + i + ']:' + arguments[i]);
        args = args.concat(arguments[i]);
    }

    var value = 0;

    for (var j = 1; j < args.length; j++) {
        value += args[j] / Math.pow(1 + rate, j);
    }
    console.log('value:' + value);
    return value;
}

function renderDeneme(value, p, record) {
    var result = 0;
    if (record.data['Kod'] == 'KMD' || record.data['Kod'] == 'KMK') {
        if (Ext.isNumeric(record.data['PersonelAdet']) && Ext.isNumeric(record.data['BirimMaliyet'])) {
            result = record.data['PersonelAdet'] / 100 * record.data['BirimMaliyet'] * Ext.getCmp('NumberFieldPersSay').getValue();
        }
        record.data['NetMaliyetNBDStr'] = result;
        record.data['NetMaliyetNBDBek'] = result;
        return Ext.util.Format.number(result, '0,000.00');
    }
    else return Ext.util.Format.number(value, '0,000.00');
}

var Common = function () { };

Common.precisionRound = function (number, precision) {
    if (Ext.isNumeric(number) && Ext.isNumeric(precision)) {
        var factor = Math.pow(10, precision);
        return Math.round(number * factor) / factor;
    }
    else return 0;
}
Common.validateNumber = function (num, min, max) {
    var result = 0;
    if (Ext.isNumeric(num)) {
        if (Ext.isNumeric(min)) {
            if (num < min) {
                result = min;
            }
            else {
                result = num;
            }
        }
        if (Ext.isNumeric(max)) {
            if (num > max) {
                result = max;
            }
            else {
                result = num;
            }
        }
    }
    else {
        result = 0;
    }
    return result;
}
Common.divideValidate = function (dividend, divisor) {
    var result = 0;
    if (divisor == 0) {
        result = 0;
    }
    else {
        result = dividend / divisor;
    }
    return result;
}

Common.searchReturnFromArray = function (array, indextoCompare, alternativeValue) {
    var found = false;
    var returnVal;
    if (array != null && array.length > 0) {
        for (var l = 0; l < array.length; l++) {
            if (array[l]['index'] == indextoCompare) {
                returnVal = array[l]['value'];
                array.splice(l, 1);
                l = array.length;
                found = true;
            }
        }
    }
    if (found == false) {
        returnVal = alternativeValue;
    }
    return returnVal;
}


//resetSerializedArray 
//serializedArr değeri boş ise, length doldurulmalı, böylece length = protokol ay kadar array initialize edilmiş olacak
//               dolu ise length doldurmaya gerek yok, dolu olan arrayi null ile sıfırlayacak.

Common.resetSerializedArray = function (serializedArr, length) {
    var unpackArr = Common.deSerializeArray(serializedArr, length, null);
    unpackArr = Common.resetArray(unpackArr, length, null);
    serializedArr = JSON.stringify(unpackArr);
    return serializedArr;
}

Common.deSerializeArray = function (serializedArr, length, value) {
    var unpackArr;
    if (typeof serializedArr === 'undefined' || serializedArr === null || serializedArr.length == 0) {
        unpackArr = Common.resetArray(unpackArr, length, value);
    }
    else {
        try {
            unpackArr = JSON.parse(serializedArr);
            if (!Array.isArray(unpackArr)) {
                throw new Error('Teknik hata( Hata #1201)');
            }
        }
        catch (err) {
            throw new Error(err.message);
        }
    }
    return unpackArr;
}

Common.SerializeArraytoHidden = function (hidden, length, array) {
    var tmpArry = [];
    if (typeof array === 'undefined' || array === null || array.length == 0) {
        tmpArry = Common.resetArray(array, length, 0);
    }
    else {
        tmpArry = array;
    }
    var returnVal = JSON.stringify(tmpArry);
    hidden.setValue(returnVal);
}

Common.modifySerializedArraytoHidden = function (hidden, index, value) {
    var serializedArr = hidden.getValue();
    var unpackArr = Common.deSerializeArray(serializedArr);
    if (!Array.isArray(unpackArr)) {
        throw new Error('Değişiklik yapılamadı( Hata #1200)');
    }
    else {
        if (unpackArr.length < index) {
            throw new Error('Değişiklik yapılamadı( Hata #1201)');
        }
        else {
            unpackArr[index] = value;
            var strng = JSON.stringify(unpackArr);
            hidden.setValue(strng);
        }
    }
}

Common.resetArray = function (Arr, length, resetValue) {
    if (!Array.isArray(Arr)) {
        if (length < 1)
            throw new Error('Teknik hata(Hata #1203) - ' + length);
        Arr = new Array(length);
    }
    else {
        if (Arr.length > 0) {
            length = Arr.length;
        }
    }
    for (var i = 0; i < length ; i++) {
        Arr[i] = resetValue;
    }
    return Arr;
}

Common.columnExists = function (grid, columnname) {
    var result = false;
    var columns = grid.headerCt.getGridColumns();
    for (i = 0, len = columns.length; i < len; i++) {
        if (columns[i].dataIndex == columnname) {
            result = true;
            break;
        }
    }
    return result;
}
//Common.decimalFormat = function (value) {
//    if (Ext.isNumeric(value)) {
//        if (value < 1) {
//            var valStr = value.toString();
//            var decimalStr = valStr.split('.')[1];
//            /*Atadığımız nümerik değer çok büyük olduğunda jscript kendi round ediyor bu nedenle indexof
//              diye bulmaya çalıştığımda bulamıyor ve formatlama yanlış dönüyor. 5 basamağı geçen nümerikleri keseceğim.*/
//            decimalStr = Number(decimalStr).toString();
//            if (decimalStr.length > 5)
//            {
//                decimalStr = decimalStr.substring(0, 5);
//            }
//            var indx = valStr.indexOf(decimalStr);
//            var format = '0.';
//            for (var i = 0; i < indx; i++) {
//                format += '0';
//            }
//            return format;
//        }
//        else {
//            return '0,000.00';
//        }
//    }
//    else {
//        return '0,000.00';
//    }
//}


function isNumeric(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}

Common.decimalFormat = function (inputVal) {
    if (isNumeric(inputVal)) {
        if (inputVal < 1) {
            //if (inputVal < 0.000001) {
            //    return '0.00000';
            //}
            //else {
            //    var valStr = inputVal.toString();
            //    var format = '0.';
            //    for (var i = 0; i < valStr.length; i += 1) {
            //        if (valStr.charAt(i) == '0' || valStr.charAt(i) == '.') {
            //            format += '0';
            //        }
            //        else {
            //            return format;
            //        }
            //    }
            //}
            return '0.00000';
        }
        else {
            return '0,000.00';
        }
    }
    else {
        return '0,000.00';
    }
}


Common.isDirtyStore = function (store) {
    var isDirty = false;
    store.each(function (item) {
        if (item.dirty == true) {
            isDirty = true;
        }
    });
    if (!isDirty) {
        isDirty = (store.removed.length > 0);
    }
    return isDirty;
}

Common.dirtyIndex = function (store) {
    var retIndex = -1;
    store.each(function (item, index) {
        if (item.dirty === true) {
            retIndex = index;
        }
    });
    return retIndex;
}


Common.includesStr = function (strToSearchFor) {
    var result = false;
    if (typeof strToSearchFor === 'undefined' || strToSearchFor === null || strToSearchFor.length == 0) {
        result = false;
    }
    else {
        var args = [];
        for (var i = 0; i < arguments.length; i++) {
            args = args.concat(arguments[i]);
        }

        for (var j = 1; j < args.length; j++) {
            var tmpStr = args[j];
            if (typeof tmpStr === 'undefined' || tmpStr === null || tmpStr.length == 0) {

            }
            else {
                if (strToSearchFor.indexOf(tmpStr) != -1) {
                    result = true;
                }
            }
        }
    }
    return result;
}

var Financial = function () { };

Financial.kredihesapla = function (ortalama, netKazanc, adet, adetToplam, stratejikPlanArtisOran, personelAdet, personeladetOran, ongorulenkbOrtalama, persadetyuzdeModified, ongorulenkbModified, ongorulenkaroranModified, stratejikkaroranModified) {
    var result = [];
    var protokolAy = Ext.getCmp('NumberFieldProtokolSure').getValue();
    var protokolAyYuzde;
    ortalama = Common.validateNumber(ortalama, 0, null);
    netKazanc = Common.validateNumber(netKazanc, 0, null);
    adet = Common.validateNumber(adet, 0, null);
    adetToplam = Common.validateNumber(adetToplam, 0, null);
    stratejikPlanArtisOran = Common.validateNumber(stratejikPlanArtisOran, 0, 100) / 100;
    personelAdet = Common.validateNumber(personelAdet, 0, null);
    personeladetOran = Common.validateNumber(personeladetOran, 0, null) / 100;
    ongorulenkbOrtalama = Common.validateNumber(ongorulenkbOrtalama, 0, null);
    //console.log('2ortalama' + ' : ' + ortalama);
    //console.log('2netKazanc' + ' : ' + netKazanc);
    //console.log('2adet' + ' : ' + adet);
    //console.log('2adetToplam' + ' : ' + adetToplam);
    //console.log('2stratejikPlanArtisOran' + ' : ' + stratejikPlanArtisOran);
    //console.log('2personelAdet' + ' : ' + personelAdet);
    //console.log('2personeladetOran' + ' : ' + personeladetOran);
    //console.log('2ongorulenkbOrtalama' + ' : ' + ongorulenkbOrtalama);

    var karOran = Common.divideValidate(netKazanc, ortalama);
    var adetOran = Common.divideValidate(adet, adetToplam);
    var kisibasiOrtalama = Common.divideValidate(ortalama, adet);
    //console.log('karOran' + ' : ' + karOran);
    //console.log('adetOran' + ' : ' + adetOran);
    //console.log('kisibasiOrtalama' + ' : ' + kisibasiOrtalama);

    var persAdYuzde = [];
    var stratejikKBOrtalama = [];

    var monthModifier = 12;
    var maxMonthByModifier = Math.ceil(protokolAy / monthModifier);
    var maxAy = maxMonthByModifier * monthModifier;
    //console.log('monthModifier' + ' : ' + monthModifier);
    //console.log('maxMonthByModifier' + ' : ' + maxMonthByModifier);
    //console.log('maxAy' + ' : ' + maxAy);

    stratejikKBOrtalamaMax = [];

    for (var i = 1; i <= maxMonthByModifier; i++) {
        var arrayPos = i * monthModifier - 1;
        if (i == 1) {
            stratejikKBOrtalamaMax[arrayPos] = kisibasiOrtalama + (kisibasiOrtalama * stratejikPlanArtisOran);
            //console.log('arrayPos' + ' : ' + arrayPos);
            //console.log('stratejikKBOrtalamaMax[arrayPos]1' + ' : ' + stratejikKBOrtalamaMax[arrayPos]);
        }
        else {
            var previousOrtalama = stratejikKBOrtalamaMax[((i - 1) * monthModifier) - 1];
            stratejikKBOrtalamaMax[arrayPos] = previousOrtalama + (previousOrtalama * stratejikPlanArtisOran);
            //console.log('stratejikKBOrtalamaMax[arrayPos]2' + ' : ' + stratejikKBOrtalamaMax[arrayPos]);
        }
    }
    lastIndex = protokolAy - 1;
    persAdYuzde[lastIndex] = Common.searchReturnFromArray(persadetyuzdeModified, lastIndex, personeladetOran);
    //console.log('lastIndex' + ' : ' + lastIndex);
    //console.log('persAdYuzde[lastIndex]' + ' : ' + persAdYuzde[lastIndex]);

    for (var i = 0; i < protokolAy; i++) {
        //console.log('----------------------------------------------------');
        //console.log('i' + ' : ' + i);
        item = {};
        var modCeilingValue = Math.ceil((i + 1) / monthModifier);
        var modFloorValue = Math.floor((i + 1) / monthModifier);
        if ((i + 1) % monthModifier == 0) {
            stratejikKBOrtalama[i] = stratejikKBOrtalamaMax[i];
        }
        else {
            var first;
            var secondTemp;
            if (i == 0) {
                first = kisibasiOrtalama;
            }
            else {
                first = stratejikKBOrtalama[i - 1];
            }
            if (modCeilingValue == 1) {
                secondTemp = kisibasiOrtalama;
            }
            else {
                secondTemp = stratejikKBOrtalamaMax[modFloorValue * monthModifier - 1];
            }
            var second = (stratejikKBOrtalamaMax[modCeilingValue * monthModifier - 1] - secondTemp) / monthModifier;
            stratejikKBOrtalama[i] = first + second;
        }
        if (i == protokolAy - 1) {
            //yukarıda atadık.
        }
        else {
            var first;
            if (i == 0) {
                first = adetOran;
                protokolAyYuzde = protokolAy;
                //console.log('first1' + ' : ' + first);
                //console.log('protokolAyYuzde' + ' : ' + protokolAyYuzde);
            }
            else {
                first = persAdYuzde[i - 1];
                //console.log('first2' + ' : ' + first);
            }
            var oranFark = (persAdYuzde[protokolAy - 1] - adetOran) / protokolAyYuzde;
            var persAdetYuzde = first + oranFark;
            //console.log('persAdYuzde[protokolAy - 1]' + ' : ' + persAdYuzde[protokolAy - 1]);
            //console.log('oranFark' + ' : ' + oranFark);
            //console.log('persAdetYuzde' + ' : ' + persAdetYuzde);
            persAdYuzde[i] = Common.searchReturnFromArray(persadetyuzdeModified, i, persAdetYuzde);
            //console.log('persAdYuzde[protokolAy - 1]' + ' : ' + persAdYuzde[protokolAy - 1]);
            if (persAdetYuzde != persAdYuzde[i]) {
                //console.log('zzzzzzzzzpersAdetYuzde:' + persAdetYuzde);
                adetOran = persAdYuzde[i];
                protokolAyYuzde = protokolAy - i;
            }
        }
        item['Ay'] = i + 1;
        item['PersonelAdetYuzde'] = persAdYuzde[i] * 100;
        item['USOKisiSayi'] = persAdYuzde[i] * personelAdet;
        item['StratejikKBOrtalama'] = stratejikKBOrtalama[i];
        item['StratejikToplamTutar'] = item['USOKisiSayi'] * item['StratejikKBOrtalama'];
        item['OngorulenKBOrtalama'] = Common.searchReturnFromArray(ongorulenkbModified, i, ongorulenkbOrtalama);
        item['OngorulenKBBakiye'] = item['USOKisiSayi'] * item['OngorulenKBOrtalama'];
        item['StratejikKarOran'] = Common.searchReturnFromArray(stratejikkaroranModified, i, karOran);
        item['StratejikNetKazanc'] = item['StratejikToplamTutar'] * karOran;
        item['BeklentiKarOran'] = Common.searchReturnFromArray(ongorulenkaroranModified, i, karOran);
        item['BeklentiNetKazanc'] = item['OngorulenKBBakiye'] * karOran;

        //console.log('item[Ay]: ' + item['Ay']);
        //console.log('item[PersonelAdetYuzde]: ' + item['PersonelAdetYuzde']);
        //console.log('item[USOKisiSayi]: ' + item['USOKisiSayi']);
        //console.log('item[StratejikKBOrtalama]: ' + item['StratejikKBOrtalama']);
        //console.log('item[StratejikToplamTutar]: ' + item['StratejikToplamTutar']);
        //console.log('item[OngorulenKBOrtalama]: ' + item['OngorulenKBOrtalama']);
        //console.log('item[OngorulenKBBakiye]: ' + item['OngorulenKBBakiye']);
        //console.log('item[StratejikKarOran]: ' + item['StratejikKarOran']);
        //console.log('item[StratejikNetKazanc]: ' + item['StratejikNetKazanc']);
        //console.log('item[BeklentiKarOran]: ' + item['BeklentiKarOran']);
        //console.log('item[BeklentiNetKazanc]: ' + item['BeklentiNetKazanc']);


        result.push(item);
    }
    return result;
}

Financial.PersonelAdetYuzdeHesapla = function (personeladetOran, adetOran, protokolAy, array) {
    var result = [];
    var lastIndex = protokolAy - 1;

    result[lastIndex] = personeladetOran;

    for (var i = 0; i < lastIndex; i++) {
        var persAdetYuzde;
        if (Array.isArray(array) && Ext.isNumeric(array[i]) && array[i] != null) {
            persAdetYuzde = array[i];
            adetOran = array[i];
            protokolAyYuzde = protokolAy - i;
        }
        else {
            var first;
            if (i == 0) {
                first = adetOran;
                protokolAyYuzde = protokolAy;
            }
            else {
                first = result[i - 1];
            }
            var oranFark = (result[lastIndex] - adetOran) / protokolAyYuzde;
            persAdetYuzde = first + oranFark;
        }
        result[i] = persAdetYuzde;
    }
    return result;
}


Common.getGridPanelData = function (grid, config) {
    var data = [],
        columns = grid.headerCt.getGridColumns(),
        config = config || {};

    Ext.each(config.currentPageOnly ? grid.store.getRange() : grid.store.getAllRange(), function (record, index) {
        var item = {}, i, len, c, meta, value;

        for (i = 0, len = columns.length; i < len; i++) {
            c = columns[i];
            if (c.dataIndex) {
                if (config.visibleOnly) {
                    if (c.hidden) {
                        continue;
                    }
                }
                if (config.headerTitle) {
                    item[c.text] = record.data[c.dataIndex];
                }
                else {
                    item[c.dataIndex] = record.data[c.dataIndex];
                }
            }
        }

        data.push(item);
    });

    return data;
};
Common.getGridPanelData2 = function (grid, config) {
    var data = [],
        columns = grid.headerCt.getGridColumns(),
        config = config || {};

    Ext.each(config.currentPageOnly ? grid.store.getRange() : grid.store.getAllRange(), function (record, index) {
        var item = {}, i, len, c, meta, value;

        for (i = 0, len = columns.length; i < len; i++) {
            c = columns[i];
            if (c.dataIndex) {
                if (config.visibleOnly) {
                    if (c.hidden) {
                        continue;
                    }
                }
                if (config.headerTitle) {
                    item[c.dataIndex] = item[c.text];
                }
                else if (config.headerDataIndex) {
                    item[c.dataIndex] = record.data[c.dataIndex];
                }
                else if (config.headerDataType) {
                    if (c.StoreModelType) {
                        item[c.dataIndex] = c.StoreModelType;
                    }
                }
            }
        }

        data.push(item);
    });

    return data;
};

//////// Renderers

