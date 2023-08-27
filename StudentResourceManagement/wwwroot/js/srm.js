$(document).ready(function () {
    $('#resourceNameInput').val('');
    $('#studentNameInput').val('');
    var selectedResource = '';
    var selectedStudent = '';

    //Events
    $('#resourceAdd').on('click', function () {
        var resourceName = $('#resourceNameInput').val();
        $.ajax({
            url: 'Resource/Add',
            method: 'POST',
            data: { resourceName: resourceName },
            success: function (response) {
                // Handle successful response
                if (response === "Resource created successfully.") {
                    $('#addResourceModal').modal('hide');
                    toastr.success(response, 'Success');
                    setTimeout(function () {
                        location.reload();
                    }, 2000);
                }
                toastr.error(response, 'Error');
                
            },
            error: function (xhr, status, error) {
                // Handle error
                toastr.error('An error occurred while processing your request.', 'Error');
            }
        });
       
    });
    $('#studentButton').on('click', function () {
        populateResourceDropdown();
    });

    $('#allocationButton').on('click', function () {
        populateStudentDropdown();
        populateResourcesDropdown();
    });
    $('#studentAdd').on('click', function () {
        var studentName = $('#studentNameInput').val();
        var resourceName = selectedResource;
        var expiryDate = $('#expiryDateInput').val();
        if (resourceName) {
            if (expiryDate) {

                $.ajax({
                    url: 'Student/Add',
                    method: 'POST',
                    data: { studentName: studentName, resourceId: resourceName, tillDate: expiryDate },
                    success: function (response) {
                        // Handle successful response
                        toastr.success(response, 'Success');
                        setTimeout(function () {
                            location.reload();
                        }, 2000);
                    },
                    error: function (xhr, status, error) {
                        // Handle error
                        toastr.error('An error occurred while processing your request.', 'Error');
                    }
                });
            }
            else 
                toastr.error('Select Till when you want to allocate this recource', 'Error');
        }
        else {
            toastr.error('Select a Resource Or add a resource first.', 'Error');
        }
    });

    $('#resourceAllocate').on('click', function () {
        var studentName = selectedStudent;
        var resourceName = selectedResource;
        var expiryDate = $('#expiryDateInputs').val();
        if (resourceName && studentName) {
            if (expiryDate) {

                $.ajax({
                    url: 'Student/AddMapping',
                    method: 'POST',
                    data: { studentId: studentName, resourceId: resourceName, tillDate: expiryDate },
                    success: function (response) {
                        // Handle successful response
                        toastr.success(response, 'Success');
                        setTimeout(function () {
                            location.reload();
                        }, 2000);
                    },
                    error: function (xhr, status, error) {
                        // Handle error
                        toastr.error('An error occurred while processing your request.', 'Error');
                    }
                });
            }
            else
                toastr.error('Select Till when you want to allocate this recource', 'Error');
        }
        else {
            toastr.error('Select All the fields.', 'Error');
        }
    });

    $('#resourceSelect').on('change', function () {
        selectedResource = $(this).val();
    });
    $('#resourcesSelect').on('change', function () {
        selectedResource = $(this).val();
    });
    $('#studentSelect').on('change', function () {
        selectedStudent = $(this).val();
    });

    function populateResourceDropdown() {
        $.get('Resource/getAllResources', function (data) {
            var studentSelect = $('#resourceSelect');

            // Clear existing options
            studentSelect.html('');

            // Add default option
            studentSelect.append($('<option>', {
                value: '',
                text: 'Select a resource'
            }));

          
            $.each(data, function (index, resourceName) {
                console.log(resourceName);
                studentSelect.append($('<option>', {
                    value: resourceName.id,
                    text: resourceName.name
                }));
            });
        });
    }

    function populateResourcesDropdown() {
        $.get('Resource/getAllResources', function (data) {
            var studentSelect = $('#resourcesSelect');

            // Clear existing options
            studentSelect.html('');

            // Add default option
            studentSelect.append($('<option>', {
                value: '',
                text: 'Select a resource'
            }));


            $.each(data, function (index, resourceName) {
                console.log(resourceName);
                studentSelect.append($('<option>', {
                    value: resourceName.id,
                    text: resourceName.name
                }));
            });
        });
    }

    function populateStudentDropdown() {
        $.get('Student/getAllStudents', function (data) {
            var studentSelect = $('#studentSelect');

            // Clear existing options
            studentSelect.html('');

            // Add default option
            studentSelect.append($('<option>', {
                value: '',
                text: 'Select a student'
            }));


            $.each(data, function (index, resourceName) {
                console.log(resourceName);
                studentSelect.append($('<option>', {
                    value: resourceName.id,
                    text: resourceName.name
                }));
            });
        });
    }
    $.get('Student/GetAllMapping', function (data) {
        var tableBody = $('#studentTable tbody');
        tableBody.empty(); // Clear existing rows

        $.each(data, function (index, record) {
            var row = $('<tr>');
            row.append($('<td>').text(record.studentName));
            row.append($('<td>').text(record.resourceName));
            row.append($('<td>').text(record.allocatedTill));
            tableBody.append(row);
        });
    });
});