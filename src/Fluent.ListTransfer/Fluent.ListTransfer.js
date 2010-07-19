
function LT_Transfer( sourceID, destinationID, selectedOnly, add, remove, allowDuplicates){

	var sourceList = document.getElementById(sourceID);
	var destinationList = document.getElementById(destinationID);

	var itemsForTransfer = new Array();

	var i;
	for(i = 0; i < sourceList.options.length; i++){
		var option = sourceList.options[i];
		if(option.selected || !selectedOnly){
			itemsForTransfer[itemsForTransfer.length] = new Option(option.text, option.value);
		}
	}
	
	if(remove){
		for(i = 0; i < sourceList.options.length; i++){
			var option = sourceList.options[i];
			if(option.selected || !selectedOnly){
				sourceList.options[i] = null;
				i--;
			}
		}
	} 

	for(i = 0; i < itemsForTransfer.length; i++){
		var option = itemsForTransfer[i];
		if(add && ( allowDuplicates || !LT_Contains( destinationList, option))){
			destinationList.options[destinationList.options.length] = option;
		}
	}
	
	LT_StoreListState(sourceList);
	LT_StoreListState(destinationList);
}

function LT_Move(listID, moveUp){
	var list = document.getElementById(listID);
	if(!moveUp){
		for(i = list.options.length-2; i >= 0; i--){
			if(list.options[i].selected && !list.options[i+1].selected ){
				var option = list.options[i];
				list.options[i] = new Option(list.options[i+1].text, list.options[i+1].value);
				list.options[i+1] = new Option(option.text, option.value);
				list.options[i+1].selected = true;
			}
		}
	} else {
		for(i = 1; i < list.options.length; i++){
			if(list.options[i].selected && !list.options[i-1].selected ){
				var option = list.options[i];
				list.options[i] = new Option(list.options[i-1].text, list.options[i-1].value);
				list.options[i-1] = new Option(option.text, option.value);
				list.options[i-1].selected = true;
			}
		}
	}
	LT_StoreListState(list);
}

function LT_Contains(list, option){
	var i;
	for(i = 0; i < list.options.length; i++){
		if(list.options[i].value == option.value){
			return true;
		}
	}
}

function LT_StoreListState(list){
	
	
	var i;
	var state = "1";
	for(i = 0; i < list.options.length; i++){
		state += "|" + list.options[i].text + "|" + list.options[i].value;
	}
	document.getElementById(list.id + "_State").value = state;
}