const { pool } = require('./dbConnection.js');

async function seedAdmin() {
  try {
    const adminData = await fs.promises.readFile('SeedData/admin_data.json', 'utf8');
    const adminInfo = JSON.parse(adminData);

    await pool.query('DELETE FROM admin');

    for (const info of adminInfo) {
      const query = `INSERT INTO admin
                     (username, password, role)
                     VALUES ($1, $2, $3)`;
      const values = [info.name, info.password, info.role];

      await pool.query(query, values);
    }

    pool.end();
  } catch (err) {
    console.error('Error seeding database:', err);
  }
}
