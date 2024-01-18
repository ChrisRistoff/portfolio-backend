const { pool } = require('./dbConnection.js');
const fs = require('fs').promises;

async function exportData() {
  try {
    // personal_info
    const personalInfoResult = await pool.query(`SELECT * FROM personal_info`);
    const personalInfoData = JSON.stringify(personalInfoResult.rows, null, 2);
    await fs.writeFile('SeedData/personal_info.json', personalInfoData);

    // projects
    const projectsResult = await pool.query(`SELECT * FROM projects`);
    const projectsData = JSON.stringify(projectsResult.rows, null, 2);
    await fs.writeFile('SeedData/projects_info.json', projectsData);

    // admin
    const adminResult = await pool.query(`SELECT * FROM admin`);
    const adminData = JSON.stringify(adminResult.rows, null, 2);
    await fs.writeFile('SeedData/admin_data.json', adminData);

    console.log('Data extracted!');
  } catch (err) {
    console.error(err);
  } finally {
    pool.end();
  }
}

exportData();
