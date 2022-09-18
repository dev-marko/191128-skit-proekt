describe('comment spec', () => {
  let count = Math.floor(Math.random() * 101);
  it('user can comment', () => {

    cy.visit('http://localhost:3000/home')

    // login
    cy.findByRole('link', {name: /најава/i}).click();
    cy.findByRole('textbox', {name: /корисничко име/i}).type('markos');
    cy.findByLabelText(/лозинка/i).type('marko123');
    cy.findByRole('button', {name: /најава/i}).click();
    
    // wait for the POST request to finish
    cy.intercept({
      method: "POST",
      url: "http://localhost:43172/api/**",
    }).as("loginFirst");
    cy.wait("@loginFirst");

    // click Сите Растенија
    cy.findByRole('link', {name: /сите растенија/i}).click();

    // choose category Цвеќиња
    cy.findByRole('link', {
      name: /цвеќиња цвеќиња lorem ipsum dolor sit amet, consectetur adipiscing elit\. in blandit risus a tellus eleifend, in bibendum neque posuere\. одбери/i
    }).findByRole('button', {
      name: /одбери/i
    }).click();

    // choose Ружа
    cy.findByRole('link', {
      name: /\[object object\] ружа lorem ipsum dolor sit amet, consectetur adipiscing elit\. in blandit risus a tellus eleifend, in bibendum neque posuere\. одбери/i
    }).findByRole('button', {
      name: /одбери/i
    }).click();
    
    // Select comment field and enter some text
    cy.findByRole('textbox').scrollIntoView();
    cy.findByRole('textbox').type(`cypress e2e test comment id: ${count}`);

    // click comment button
    cy.findByRole('button', { name: /коментирај/i}).click();

    // verify if comment was written
    cy.findByText(`cypress e2e test comment id: ${count}`).should('be.visible');
  })
})