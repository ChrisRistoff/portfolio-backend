const { pool } = require('./dbConnection.js');
const fs = require('fs').promises;

async function exportData() {
  try {
    // Query personal_info and write to file
    const personalInfoResult = await pool.query(`SELECT * FROM personal_info`);
    const personalInfoData = JSON.stringify(personalInfoResult.rows, null, 2);
    await fs.writeFile('SeedData/personal_info.json', personalInfoData);

    // Query projects and write to file
    const projectsResult = await pool.query(`SELECT * FROM projects`);
    const projectsData = JSON.stringify(projectsResult.rows, null, 2);
    await fs.writeFile('SeedData/projects_info.json', projectsData);

  } catch (err) {
    console.error(err);
  } finally {
    pool.end();
  }
}

exportData();
