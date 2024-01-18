const { pool } = require('./dbConnection.js');
const fs = require('fs');

async function seedDatabase() {
  try {
    const data = await fs.promises.readFile('SeedData/personal_info.json', 'utf8');
    const personalInfo = JSON.parse(data);

    // Clear the existing data in the table
    await pool.query('DELETE FROM personal_info');

    // Insert new data
    for (const info of personalInfo) {
      const query = `INSERT INTO personal_info
                     (id, name, title, email, bio, github, linkedin, image)
                     VALUES ($1, $2, $3, $4, $5, $6, $7, $8)`;
      const values = [info.id, info.name, info.title, info.email, info.bio, info.github, info.linkedin, info.image];

      await pool.query(query, values);
    }

    console.log('Personal Info table seeded!');

    // Repeat for projects table
    const projectsData = await fs.promises.readFile('SeedData/projects_info.json', 'utf8');
    const projectsInfo = JSON.parse(projectsData);

    await pool.query('DELETE FROM projects');

    for (const info of projectsInfo) {
      const query = `INSERT INTO projects
                     (id, name, tagline, description, image, repo, link, tech_stack, project_type)
                     VALUES ($1, $2, $3, $4, $5, $6, $7, $8, $9)`;
      const values = [info.id, info.name, info.tagline, info.description, info.image, info.repo, info.link, info.tech_stack, info.project_type];

      await pool.query(query, values);
    }

    console.log('Projects table seeded!');

    pool.end();
  } catch (err) {
    console.error('Error seeding database:', err);
  }
}

seedDatabase();
