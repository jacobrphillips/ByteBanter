let index = 0;

function AddTag() {
    //Get reference of the TagEntry input element using DOM
    var tagEntry = document.getElementById("TagEntry");

    //Use Search function to detect error state
    let searchResult = Search(tagEntry.value);

    if (searchResult != null) {
        //Trigger SweetAlert for the empty string or duplicate
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Empty Tags are not permitted',
        })
    }
    else {
        //Create new select option
        let newOption = new Option(tagEntry.value, tagEntry.value);
        document.getElementById("TagList").options[index++] = newOption;
    }

    //Clear out the TagEntry control
    tagEntry.value = "";
    return true;
}

function DeleteTag() {
    let tagCount = 1;
    let tagList = document.getElementById("TagList");
    if (!tagList) {
        return false;
    }
    if (tagList.selectedIndex == -1) {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Choose a Tag before deleting!',
        });
        return true;
    }
    while (tagCount > 0) {
        if (tagList.selectedIndex >= 0) {
            tagList.options[tagList.selectedIndex] = null;
            --tagCount;
        }
        else {
            tagCount = 0;
            index--;
        }
    }
}

$("form").on("submit", function () {
    $("#TagList option").prop("selected", "selected");
});

//Look for the tagValues variable to see if it has data
if (tagValues != '') {
    let tagArray = tagValues.split(",");
    for (let loop = 0; loop < tagArray.length; loop++) {
        //Load up and replace the options that we have
        ReplaceTag(tagArray[loop], loop)
        index++;
    }
}


function ReplaceTag(tag, index) {
    let newOption = new Option(tag, tag);
    document.getElementById("TagList").options[index] = newOption;
}


//The Search function will detect either an empty or a duplicate Tag
//and Return an error string if an error is detected
function Search(str) {
    if (str == "") {
        return 'Empty tags are not permitted';
    }

    var tagsEl = document.getElementById('TagList');
    if (tagsEl) {
        let options = tagsEl.options;
        for (let index = 0; index < options.length; index++) {
            if (options[index].value == str) {
                return `The Tag #${str} was detected as a duplicate and not permitted`;
            }
        }
    }
}

//SweetAlert window
const swalWithDarkButton = Swal.mixin({
    cutomClass: {
        confirmButton: 'btn btn-danger btn-sm w-100 btn-outline-dark'
    },
    timer: 3000,
    buttonsStyling: false
})