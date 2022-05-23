function CloseModal(modalId){
    $(modalId).modal('hide')
}

function ShowModal(modalId){
    $(modalId).modal('show')
}

function ResetForm(frmId){
    $(frmId).trigger('reset')
}