document.getElementById('add-task').addEventListener('click', function() {
    const taskText = document.getElementById('new-task').value;
    const startDate = document.getElementById('start-date').value;
    const endDate = document.getElementById('end-date').value;
    if (taskText === '') return;

    const li = document.createElement('li');
    li.textContent = taskText;

    const taskDetails = document.createElement('div');
    taskDetails.className = 'task-details';
    taskDetails.innerHTML = `
        <span>Start Date: ${startDate}</span>
        <span>End Date: ${endDate}</span>
    `;

    const completeButton = document.createElement('button');
    completeButton.textContent = 'Complete';
    completeButton.addEventListener('click', function() {
        li.classList.toggle('completed');
    });

    const deleteButton = document.createElement('button');
    deleteButton.textContent = 'Delete';
    deleteButton.addEventListener('click', function() {
        li.remove();
    });

    li.appendChild(taskDetails);
    li.appendChild(completeButton);
    li.appendChild(deleteButton);

    document.getElementById('task-list').appendChild(li);
    document.getElementById('new-task').value = '';
    document.getElementById('start-date').value = '';
    document.getElementById('end-date').value = '';
});
