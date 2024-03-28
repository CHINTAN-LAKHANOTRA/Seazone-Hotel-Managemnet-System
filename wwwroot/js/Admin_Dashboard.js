function handleContainerChange(container) {
    fetch('/Admin_Dashboard/ChangeContainer?container=' + container, {
        method: 'POST'
    }).then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json();
    }).then(data => {
        console.log(data);
    }).catch(error => {
        console.error('There was a problem with your fetch operation:', error);
    });
}
