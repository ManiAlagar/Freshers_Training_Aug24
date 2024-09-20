
function DynamicBox(event)
{
    event.preventDefault();
    document.getElementById('inputFields').innerHTML = '<div id="inputFields"></div>'
    
    let inputValue = document.getElementsByClassName("input");
    let ArrayCollection = inputValue[0].value.trim().replace(/\s+/g , '').split(",");
     
    for(i=0;i<ArrayCollection.length;i++)
        {
            if(ArrayCollection[i].trim() != '')
            {
                document.getElementById("inputFields").innerHTML  += '<br> <input readonly type="text" value='+ ArrayCollection[i] +'> <br>';
            }     
        } 
}
function ResetAll()
{
    document.getElementById('inputFields').innerHTML = '<div id="inputFields"></div>'
}

